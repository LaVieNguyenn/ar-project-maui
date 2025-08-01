using ARMauiApp.Models;
using ARMauiApp.Services;
using ARMauiApp;

public partial class CartViewModel : ObservableObject
{
    private readonly CartService _cartService;

    [ObservableProperty] private ObservableCollection<CartItemDto> cartItems = new();
    [ObservableProperty] private decimal totalAmount = 0;
    [ObservableProperty] private bool isLoading = false;
    [ObservableProperty] private bool isRefreshing = false;
    [ObservableProperty]
    private bool isTryOnEnabled = true;
    public CartViewModel(CartService cartService)
    {
        _cartService = cartService;
    }

    [RelayCommand]
    public async Task RefreshCart()
    {
        System.Diagnostics.Debug.WriteLine($"RefreshCart called, IsLoading={IsLoading}");
        if (IsLoading) return;
        IsLoading = true;
        try
        {
            var cart = await _cartService.GetCartAsync();
            CartItems.Clear();
            decimal total = 0;
            if (cart?.Items != null)
            {
                foreach (var item in cart.Items)
                {
                    CartItems.Add(item);
                    total += item.Price * item.Quantity;
                }
            }
            TotalAmount = total;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Lỗi", $"Không thể tải giỏ hàng: {ex.Message}", "OK");
        }
        finally
        {
            System.Diagnostics.Debug.WriteLine($"CartItems.Count = {CartItems.Count} after refresh");
            IsLoading = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    public async Task RemoveItem(string productId)
    {
        if (IsLoading) return;
        var confirm = await Shell.Current.DisplayAlert("Xóa sản phẩm", "Bạn chắc chắn muốn xóa sản phẩm này khỏi giỏ hàng?", "Đồng ý", "Hủy");
        if (!confirm) return;

        IsLoading = true;
        var result = await _cartService.RemoveCartItemAsync(productId);
        if (result)
        {
            var item = CartItems.FirstOrDefault(x => x.ProductId == productId);
            if (item != null) CartItems.Remove(item);
            TotalAmount = CartItems.Sum(i => i.Price * i.Quantity);
        }
        else
        {
            await Shell.Current.DisplayAlert("Lỗi", "Không xóa được sản phẩm khỏi giỏ.", "OK");
        }
        IsLoading = false;
    }

    [RelayCommand]
    public async Task DecreaseQuantity(string productId)
    {
        if (IsLoading) return;
        var item = CartItems.FirstOrDefault(x => x.ProductId == productId);
        if (item == null || item.Quantity <= 1) return;

        IsLoading = true;
        var result = await _cartService.UpdateCartItemAsync(productId, item.Quantity - 1);
        if (result != null)
        {
            item.Quantity -= 1;
            TotalAmount = CartItems.Sum(i => i.Price * i.Quantity);
        }
        IsLoading = false;
    }

    [RelayCommand]
    public async Task IncreaseQuantity(string productId)
    {
        if (IsLoading) return;
        var item = CartItems.FirstOrDefault(x => x.ProductId == productId);
        if (item == null) return;

        IsLoading = true;
        var result = await _cartService.UpdateCartItemAsync(productId, item.Quantity + 1);
        if (result != null)
        {
            item.Quantity += 1;
            TotalAmount = CartItems.Sum(i => i.Price * i.Quantity);
        }
        IsLoading = false;
    }

    [RelayCommand]
    public async Task TryOnCart()
    {
        if (IsLoading || CartItems.Count == 0)
        {
            await Shell.Current.DisplayAlert("Thông báo", "Giỏ hàng trống!", "OK");
            return;
        }

        var accessories = CartItems
            .Select(i => i.Model3DUrl)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct()
            .ToList();

        if (accessories.Count == 0)
        {
            await Shell.Current.DisplayAlert("Thông báo", "Không có sản phẩm nào có Model3DUrl!", "OK");
            return;
        }

#if ANDROID
        var accessoryNames = string.Join(",", accessories);
        var intent = new Android.Content.Intent(Platform.CurrentActivity, typeof(UnityActivity));
        intent.AddFlags(Android.Content.ActivityFlags.ReorderToFront);
        intent.PutExtra("accessoryName", accessoryNames);
        Platform.CurrentActivity.StartActivity(intent);
#endif
    }
}
