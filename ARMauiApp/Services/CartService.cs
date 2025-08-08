using ARMauiApp.Configuration;
using ARMauiApp.Models;

namespace ARMauiApp.Services
{
    public class CartService : BaseApiService
    {
        public CartService(TokenService tokenService) : base(tokenService) { }

        // ====== NEW: cache & event ======
        public int LastCount { get; private set; }
        public event EventHandler<int>? CountChanged;

        // Tùy shape của CartDto mà tính count cho đúng.
        // Mặc định: cộng dồn Quantity của các item.
        private static int ComputeCount(CartDto? cart)
        {
            // TODO: sửa theo model của bạn.
            // Ví dụ nếu CartDto có List<CartItemDto> Items { get; set; } và mỗi item có int Quantity:
            return cart?.Items?.Sum(i => i.Quantity) ?? 0;

            // Nếu API đã trả sẵn tổng: return cart?.TotalQuantity ?? 0;
        }

        private void UpdateCount(CartDto? cart)
        {
            var c = ComputeCount(cart);
            if (c != LastCount)
            {
                LastCount = c;
                Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(
                    () => CountChanged?.Invoke(this, c));
            }
        }
        // =================================

        public async Task<CartDto?> GetCartAsync()
        {
            var cart = await GetAsync<CartDto>(ApiConfig.Endpoints.Cart);
            UpdateCount(cart);                // NEW
            return cart;
        }

        public async Task<CartDto?> AddToCartAsync(string productId, int quantity = 1)
        {
            var body = new { ProductId = productId, Quantity = quantity };
            var cart = await PostAsync<CartDto>($"{ApiConfig.Endpoints.Cart}", body);
            UpdateCount(cart);                // NEW
            return cart;
        }

        public async Task<CartDto?> UpdateCartItemAsync(string productId, int quantity)
        {
            var body = new { ProductId = productId, Quantity = quantity };
            var cart = await PutAsync<CartDto>($"{ApiConfig.Endpoints.Cart}", body);
            UpdateCount(cart);                // NEW
            return cart;
        }

        public async Task<bool> RemoveCartItemAsync(string productId)
        {
            var ok = await DeleteAsync($"{ApiConfig.Endpoints.Cart}/{productId}");
            if (ok) { var cart = await GetCartAsync(); /* GetCartAsync sẽ tự UpdateCount */ }
            return ok;
        }

        public async Task<bool> ClearCartAsync()
        {
            var ok = await DeleteAsync($"{ApiConfig.Endpoints.Cart}/clear");
            if (ok) { LastCount = 0; CountChanged?.Invoke(this, 0); }
            return ok;
        }

        // Tiện ích nếu bạn chỉ muốn lấy số mà không cần toàn giỏ:
        public async Task<int> RefreshCountAsync()
        {
            var cart = await GetCartAsync();
            return LastCount; // UpdateCount() đã chạy trong GetCartAsync
        }
    }
}
