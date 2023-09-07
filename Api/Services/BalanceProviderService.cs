using Stripe;

namespace Api.Services
{
    public class BalanceProviderService : IBalanceProviderService
    {
        private readonly IStripeConfigurationService _stripeConfigurationService;

        public BalanceProviderService(IStripeConfigurationService stripeConfigurationService)
        {
            _stripeConfigurationService = stripeConfigurationService;
        }

        public async Task<Balance> GetBalance()
        {
            string balanceApiKey = _stripeConfigurationService.BalanceApiKey;
            StripeConfiguration.ApiKey = balanceApiKey;

            var service = new BalanceService();
            Balance balance = await service.GetAsync();

            return balance;
        }
    }
}
