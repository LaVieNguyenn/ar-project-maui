using ARMauiApp.Configuration;
using ARMauiApp.Models;
using System.Text.Json;

namespace ARMauiApp.Services
{
    public abstract class BaseApiService : IDisposable
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _jsonOptions;
        protected readonly TokenService _tokenService;

        protected BaseApiService(TokenService tokenService)
        {
            _tokenService = tokenService;
#if ANDROID && DEBUG
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(ApiConfig.BaseUrl),
                Timeout = TimeSpan.FromSeconds(ApiConfig.HttpClient.TimeoutSeconds)
            };
#else
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(ApiConfig.BaseUrl),
                Timeout = TimeSpan.FromSeconds(ApiConfig.HttpClient.TimeoutSeconds)
            };
#endif

            // Add default headers
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "ARMauiApp/1.0");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                Converters = {
                    new System.Text.Json.Serialization.JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                }
            };
        }

        private async Task<HttpRequestMessage> CreateRequestAsync(HttpMethod method, string endpoint, object? data = null)
        {
            var request = new HttpRequestMessage(method, endpoint);

            // Add authorization token
            var token = await _tokenService.GetTokenAsync();
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            // Add content for POST/PUT requests
            if (data != null && (method == HttpMethod.Post || method == HttpMethod.Put))
            {
                var json = JsonSerializer.Serialize(data, _jsonOptions);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            }

            return request;
        }

        private async Task<ApiResponse<T>?> SendRequestAsync<T>(HttpRequestMessage request)
        {
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ApiResponse<T>>(content, _jsonOptions);
        }

        protected async Task<T?> GetAsync<T>(string endpoint) where T : class
        {
            try
            {
                using var request = await CreateRequestAsync(HttpMethod.Get, endpoint);
                var apiResponse = await SendRequestAsync<T>(request);

                if (apiResponse?.Success == true)
                {
                    return apiResponse.Data;
                }

                System.Diagnostics.Debug.WriteLine($"API Error in GetAsync<{typeof(T).Name}>: {apiResponse?.Message ?? "Unknown error"}");
                return null;
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"HTTP Error in GetAsync<{typeof(T).Name}>: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON Error in GetAsync<{typeof(T).Name}>: {ex.Message}");
                return null;
            }
        }

        protected async Task<List<T>> GetListAsync<T>(string endpoint) where T : class
        {
            try
            {
                using var request = await CreateRequestAsync(HttpMethod.Get, endpoint);
                var apiResponse = await SendRequestAsync<List<T>>(request);

                if (apiResponse?.Success == true && apiResponse.Data != null)
                {
                    return apiResponse.Data;
                }

                System.Diagnostics.Debug.WriteLine($"API Error in GetListAsync<{typeof(T).Name}>: {apiResponse?.Message ?? "Unknown error"}");
                return new List<T>();
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"HTTP Error in GetListAsync<{typeof(T).Name}>: {ex.Message}");
                return new List<T>();
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON Error in GetListAsync<{typeof(T).Name}>: {ex.Message}");
                return new List<T>();
            }
        }

        protected async Task<T?> PostAsync<T>(string endpoint, object? data = null) where T : class
        {
            try
            {
                using var request = await CreateRequestAsync(HttpMethod.Post, endpoint, data);
                var apiResponse = await SendRequestAsync<T>(request);

                if (apiResponse?.Success == true)
                {
                    return apiResponse.Data;
                }

                System.Diagnostics.Debug.WriteLine($"API Error in PostAsync<{typeof(T).Name}>: {apiResponse?.Message ?? "Unknown error"}");
                return null;
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"HTTP Error in PostAsync<{typeof(T).Name}>: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON Error in PostAsync<{typeof(T).Name}>: {ex.Message}");
                return null;
            }
        }

        protected async Task<bool> PostAsync(string endpoint, object? data = null)
        {
            try
            {
                using var request = await CreateRequestAsync(HttpMethod.Post, endpoint, data);
                var apiResponse = await SendRequestAsync<object>(request);

                if (apiResponse?.Success == true)
                {
                    return true;
                }

                System.Diagnostics.Debug.WriteLine($"API Error in PostAsync: {apiResponse?.Message ?? "Unknown error"}");
                return false;
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"HTTP Error in PostAsync: {ex.Message}");
                return false;
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON Error in PostAsync: {ex.Message}");
                return false;
            }
        }

        protected async Task<T?> PutAsync<T>(string endpoint, object? data = null) where T : class
        {
            try
            {
                using var request = await CreateRequestAsync(HttpMethod.Put, endpoint, data);
                var apiResponse = await SendRequestAsync<T>(request);

                if (apiResponse?.Success == true)
                {
                    return apiResponse.Data;
                }

                System.Diagnostics.Debug.WriteLine($"API Error in PutAsync<{typeof(T).Name}>: {apiResponse?.Message ?? "Unknown error"}");
                return null;
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"HTTP Error in PutAsync<{typeof(T).Name}>: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON Error in PutAsync<{typeof(T).Name}>: {ex.Message}");
                return null;
            }
        }

        protected async Task<bool> PutAsync(string endpoint, object? data = null)
        {
            try
            {
                using var request = await CreateRequestAsync(HttpMethod.Put, endpoint, data);
                var apiResponse = await SendRequestAsync<object>(request);

                if (apiResponse?.Success == true)
                {
                    return true;
                }

                System.Diagnostics.Debug.WriteLine($"API Error in PutAsync: {apiResponse?.Message ?? "Unknown error"}");
                return false;
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"HTTP Error in PutAsync: {ex.Message}");
                return false;
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON Error in PutAsync: {ex.Message}");
                return false;
            }
        }

        protected async Task<bool> DeleteAsync(string endpoint)
        {
            try
            {
                using var request = await CreateRequestAsync(HttpMethod.Delete, endpoint);
                var apiResponse = await SendRequestAsync<object>(request);

                if (apiResponse?.Success == true)
                {
                    return true;
                }

                System.Diagnostics.Debug.WriteLine($"API Error in DeleteAsync: {apiResponse?.Message ?? "Unknown error"}");
                return false;
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine($"HTTP Error in DeleteAsync: {ex.Message}");
                return false;
            }
            catch (JsonException ex)
            {
                System.Diagnostics.Debug.WriteLine($"JSON Error in DeleteAsync: {ex.Message}");
                return false;
            }
        }

        public virtual void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}