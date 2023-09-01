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

        IEnumerable<BalanceTransaction> IBalanceTransactionsService.GetAllBalanceTransactions()
        {
            StripeConfiguration.ApiKey = _stripeConfigurationService.BalanceApiKey;

            var options = new BalanceTransactionListOptions
            {
                Limit = int.MaxValue
            };
            var service = new BalanceTransactionService();
            StripeList<BalanceTransaction> balanceTransactions = service.List(options);

            return balanceTransactions;
        }
    }
}
