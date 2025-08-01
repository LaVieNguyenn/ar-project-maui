namespace ARMauiApp.Services
{
    public class TokenService
    {
        private const string TOKEN_KEY = "auth_token";

        public async Task SaveTokenAsync(string token)
        {
            await SecureStorage.SetAsync(TOKEN_KEY, token);
            // Lưu thời gian hết hạn (7 ngày từ bây giờ)
            var expiry = DateTime.UtcNow.AddDays(7);
        }

        public async Task<string?> GetTokenAsync()
        {
            return await SecureStorage.GetAsync(TOKEN_KEY);
        }

        public Task ClearTokenAsync()
        {
            SecureStorage.Remove(TOKEN_KEY);
            return Task.CompletedTask;
        }

        public async Task<bool> IsLoggedInAsync()
        {
            var token = await GetTokenAsync();
            if (string.IsNullOrEmpty(token))
                return false;

            return true;
        }

        public async Task<bool> IsTokenValidAsync()
        {
            return await IsLoggedInAsync();
        }
    }
}
