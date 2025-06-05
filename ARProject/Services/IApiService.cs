using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARProject.Services
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(string endpoint);
        Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
        Task<bool> PutAsync<TRequest>(string endpoint, TRequest data);
        Task<bool> DeleteAsync(string endpoint);
        void SetBearerToken(string token);
    }
}
