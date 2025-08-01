using ARMauiApp.Configuration;
using ARMauiApp.Models;

namespace ARMauiApp.Services
{
    public class ProductService : BaseApiService
    {
        public ProductService(TokenService tokenService) : base(tokenService)
        {
        }

        public async Task<List<ProductDto>> GetProductsAsync(string categoryId, string productName)
        {
            var queryParam = $"?categoryId={Uri.EscapeDataString(categoryId)}&productName={Uri.EscapeDataString(productName)}";
            return await GetListAsync<ProductDto>(ApiConfig.Endpoints.Products + queryParam);
        }

        public async Task<ProductDto?> GetProductByIdAsync(string id)
        {
            return await GetAsync<ProductDto>($"{ApiConfig.Endpoints.Products}/{id}");
        }

        public async Task<List<ProductDto>> SearchProductsAsync(string query)
        {
            var searchParam = !string.IsNullOrWhiteSpace(query) ? $"?search={Uri.EscapeDataString(query)}" : "";
            var products = await GetListAsync<ProductDto>($"{ApiConfig.Endpoints.Products}{searchParam}");

            // Nếu API không hỗ trợ search, filter ở client side
            if (!string.IsNullOrWhiteSpace(query) && products.Any())
            {
                products = products.Where(p =>
                    p.Name.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                    p.Description.Contains(query, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            return products;
        }

        public async Task<List<ProductDto>> GetProductsByCategoryAsync(string categoryId)
        {
            var queryParam = !string.IsNullOrWhiteSpace(categoryId) ? $"?categoryId={categoryId}" : "";
            var products = await GetListAsync<ProductDto>($"{ApiConfig.Endpoints.Products}{queryParam}");

            // Nếu có categoryId, filter thêm ở client side để đảm bảo
            if (!string.IsNullOrWhiteSpace(categoryId))
            {
                products = products.Where(p =>
                    string.Equals(p.CategoryId, categoryId, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return products;
        }
    }
}
