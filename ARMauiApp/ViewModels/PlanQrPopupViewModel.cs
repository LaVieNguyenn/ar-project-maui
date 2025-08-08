using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ARMauiApp.Models;

namespace ARMauiApp.ViewModels
{
    public partial class PlanQrPopupViewModel : ObservableObject
    {
        [ObservableProperty] private string qrCode;
        [ObservableProperty] private decimal amount;
        [ObservableProperty] private string adminAccountNumber;
        [ObservableProperty] private string paymentContent;
        [ObservableProperty] private ImageSource? qrImage;

        public Action? ClosePopupAction { get; set; }


        public PlanQrPopupViewModel(PlanQrDto dto)
        {
            qrCode = dto.QrCode;
            amount = dto.Amount;
            adminAccountNumber = dto.AdminAccountNumber;
            paymentContent = dto.PaymentContent;
        }

        [RelayCommand]
        public async Task CloseAsync()
        {
            ClosePopupAction?.Invoke();
        }

        [RelayCommand]
        public async Task ShowFullContentAsync()
        {
            await Shell.Current.DisplayAlert("Nội dung chuyển khoản", PaymentContent, "Đóng");
        }


        [RelayCommand]
        private async Task CopyPaymentContentAsync()
        {
            await Clipboard.SetTextAsync(PaymentContent);
            await Shell.Current.DisplayAlert("Đã copy", "Nội dung chuyển khoản đã được copy", "OK");
        }
    }
}
