using ARMauiApp.Configuration;
using ARMauiApp.Models;

namespace ARMauiApp.Services
{
    public class PlanService : BaseApiService
    {
        public PlanService(TokenService tokenService) : base(tokenService) { }

        public async Task<List<PlanDto>> GetPlansAsync()
        {
            return await GetListAsync<PlanDto>(ApiConfig.Endpoints.Plans);
        }
    }
}
