using Stripe;

namespace Api.Services
{
    public class BalanceTransactionsService : IBalanceTransactionsService
    {
        private readonly IStripeConfigurationService _stripeConfigurationService;

        public BalanceTransactionsService(IStripeConfigurationService stripeConfigurationService)
        {
            _stripeConfigurationService = stripeConfigurationService;
        }

        async Task<IEnumerable<BalanceTransaction>> IBalanceTransactionsService.GetAllBalanceTransactions()
        {
            StripeConfiguration.ApiKey = _stripeConfigurationService.BalanceApiKey;

            var options = new BalanceTransactionListOptions
            {
                Limit = int.MaxValue
            };
            var service = new BalanceTransactionService();
            StripeList<BalanceTransaction> balanceTransactions = await service.ListAsync(options);

            return balanceTransactions;
        }
    }
}
