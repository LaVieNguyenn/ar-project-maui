using ARMauiApp.Configuration;
using ARMauiApp.Models;

namespace ARMauiApp.Services
{
    public class CartService : BaseApiService
    {
        public CartService(TokenService tokenService) : base(tokenService) { }

        public async Task<CartDto?> GetCartAsync()
        {
            return await GetAsync<CartDto>(ApiConfig.Endpoints.Cart);
        }

        public async Task<CartDto?> AddToCartAsync(string productId, int quantity = 1)
        {
            var body = new
            {
                ProductId = productId,
                Quantity = quantity
            };
            return await PostAsync<CartDto>($"{ApiConfig.Endpoints.Cart}", body);
        }

        public async Task<CartDto?> UpdateCartItemAsync(string productId, int quantity)
        {
            var body = new
            {
                ProductId = productId,
                Quantity = quantity
            };
            return await PutAsync<CartDto>($"{ApiConfig.Endpoints.Cart}", body);
        }

        public async Task<bool> RemoveCartItemAsync(string productId)
        {
            return await DeleteAsync($"{ApiConfig.Endpoints.Cart}/{productId}");
        }

        public async Task<bool> ClearCartAsync()
        {
            return await DeleteAsync($"{ApiConfig.Endpoints.Cart}/clear");
        }
    }
}
