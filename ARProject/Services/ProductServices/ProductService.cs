using ARProject.Helpers;
using ARProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARProject.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IApiService _apiService;

        public ProductService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public Task<List<Product>> GetAllProductsAsync()
            => _apiService.GetAsync<List<Product>>(ConstData.Api.BASEPRODUCT);

        public Task<Product> GetProductByIdAsync(string id)
            => _apiService.GetAsync<Product>($"{ConstData.Api.BASEPRODUCT}/{id}");

        public Task<Product> CreateProductAsync(Product product)
            => _apiService.PostAsync<Product, Product>(ConstData.Api.BASEPRODUCT, product);

        public Task<bool> UpdateProductAsync(Product product)
            => _apiService.PutAsync($"{ConstData.Api.BASEPRODUCT}/{product.Id}", product);

        public Task<bool> DeleteProductAsync(string id)
            => _apiService.DeleteAsync($"{ConstData.Api.BASEPRODUCT}/{id}");
    }
}
