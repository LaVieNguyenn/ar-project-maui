using ARProject.Helpers;
using ARProject.Models;
using System.Reflection.Metadata;

namespace ARProject.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IApiService _apiService;

        public UserService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public Task<User> CreateProductAsync(User dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteProductAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> GetProductByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<string>> LoginAsync(LoginRequest rq) =>_apiService.PostAsync<LoginRequest,ApiResponse<string>>(ConstData.Api.LOGIN, rq);
        

        public Task<bool> UpdateProductAsync(string id, User dto)
        {
            throw new NotImplementedException();
        }
    }
}
