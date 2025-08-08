using ARMauiApp.Models;
using ARMauiApp.Pages;
using ARMauiApp.Services;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ARMauiApp.ViewModels
{
    public partial class PlanListViewModel : ObservableObject
    {
        private readonly PlanService _planService;
        private readonly UserService _userService;
        private readonly PlanPaymentService _paymentService;
        private readonly TokenService _tokenService;

        public PlanListViewModel(
            PlanService planService,
            UserService userService,
            PlanPaymentService paymentService,
            TokenService tokenService)
        {
            _planService = planService;
            _userService = userService;
            _paymentService = paymentService;
            _tokenService = tokenService;

            BuyPlanCommand = new AsyncRelayCommand<PlanDto?>(BuyPlanAsync);
            RefreshCommand = new AsyncRelayCommand(LoadAsync);
        }

        #region Bindable Properties

        [ObservableProperty]
        private ObservableCollection<PlanDto> plans = new();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string? currentPlanId;

        [ObservableProperty]
        private bool isRefreshing;

        #endregion

        #region Commands

        public IAsyncRelayCommand<PlanDto?> BuyPlanCommand { get; }
        public IAsyncRelayCommand RefreshCommand { get; }

        #endregion

        /// <summary>
        /// Gọi ở OnAppearing
        /// </summary>
        public async Task Initialize()
        {
            if (Plans.Count == 0)
                await LoadAsync();
        }

        private async Task LoadAsync()
        {
            if (IsLoading) return;

            IsLoading = true;
            try
            {
                // Load song song plans + profile
                var plansTask = _planService.GetPlansAsync();
                var meTask = _userService.GetCurrentUserAsync(); 

                await Task.WhenAll(plansTask, meTask);

                var list = plansTask.Result ?? new List<PlanDto>();
                var me = meTask.Result;
                CurrentPlanId = me?.PlanId;

                // Nếu có plan hiện tại -> đưa lên đầu
                if (!string.IsNullOrWhiteSpace(CurrentPlanId))
                {
                    var current = list.FirstOrDefault(p => p.Id == CurrentPlanId);
                    if (current != null)
                    {
                        list.Remove(current);
                        list.Insert(0, current);
                    }
                }

                // Gán màu gradient theo index
                var keys = new[] { "GradientPurple", "GradientOrange", "GradientAqua" };
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].UiBrushKey = keys[i % keys.Length];
                }

                // Đẩy vào ObservableCollection
                Plans.Clear();
                foreach (var p in list)
                    Plans.Add(p);
            }
            catch (Exception ex)
            {
                await MainThread.InvokeOnMainThreadAsync(async () =>
                    await Shell.Current.DisplayAlert("Lỗi", $"Không thể tải gói dịch vụ: {ex.Message}", "OK"));
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        private async Task BuyPlanAsync(PlanDto? plan)
        {
            if (plan == null) return;

            try
            {
                IsLoading = true;

                // Gọi API generate QR theo plan
                // PaymentService.Post -> trả về PlanQrDto? (đã unwrap ApiResponse trong BaseApiService)
                var qr = await _paymentService.GenerateQrAsync(plan.Id);
                if (qr == null)
                {
                    await Shell.Current.DisplayAlert("Thất bại", "Không tạo được mã QR. Vui lòng thử lại.", "OK");
                    return;
                }

                // Mở popup QR
                var popupVm = new PlanQrPopupViewModel(qr);
                var popup = new PlanQrPopup(popupVm);

                await MainThread.InvokeOnMainThreadAsync(async () =>
                {
                    popupVm.ClosePopupAction = () => popup.Close();
                    await Shell.Current.ShowPopupAsync(popup);
                });
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Lỗi", $"Không thể tạo QR: {ex.Message}", "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
