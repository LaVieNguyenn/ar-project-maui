using ARProject.Helpers;
using ARProject.Models;

namespace ARProject.Services.UserServices
{
    public interface IUserService
    {
        Task<ApiResponse<string>> LoginAsync(LoginRequest rq);
        Task<List<User>> GetAllProductsAsync();
        Task<User> GetProductByIdAsync(string id);
        Task<User> CreateProductAsync(User dto);
        Task<bool> UpdateProductAsync(string id, User dto);
        Task<bool> DeleteProductAsync(string id);
    }
}
