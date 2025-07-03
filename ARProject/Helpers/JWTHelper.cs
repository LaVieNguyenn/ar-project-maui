using System.Text.Json;

namespace ARProject.Helpers
{
    public static class JwtHelper
    {
        public static string ExtractUserId(string token)
        {
            if (string.IsNullOrWhiteSpace(token)) return null;

            try
            {
                var payload = token.Split('.')[1];
                var json = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(PadBase64(payload)));

                var data = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

                if (data != null && data.ContainsKey("id"))
                    return data["id"]?.ToString();

                return null;
            }
            catch
            {
                return null;
            }
        }

        private static string PadBase64(string base64)
        {
            // JWT payload đôi khi thiếu padding
            return base64.PadRight(base64.Length + (4 - base64.Length % 4) % 4, '=');
        }
    }
}
