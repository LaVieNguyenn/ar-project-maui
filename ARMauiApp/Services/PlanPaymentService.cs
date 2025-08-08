using ARMauiApp.Configuration;
using ARMauiApp.Models;

namespace ARMauiApp.Services
{
    public class PlanPaymentService : BaseApiService
    {
        public PlanPaymentService(TokenService tokenService) : base(tokenService) { }

        public async Task<PlanQrDto> GenerateQrAsync(string planId)
        {
            var payload = new { PlanId = planId };
            return await PostAsync<PlanQrDto>(ApiConfig.Endpoints.PaymentQR, payload);
        }
    }
}
