namespace Api.Services
{
    public class StripeConfigurationService : IStripeConfigurationService
    {
        private const string STRIPE_BALANCE_API_KEY_SECRET_MANAGER_KEY = "Stripe:BalanceApiKey";
        private const string STRIPE_BALANCE_TRANSACTIONS_API_KEY_SECRET_MANAGER_KEY = "Stripe:BalanceTransactionsApiKey";

        private readonly IConfiguration _config;

        public StripeConfigurationService(IConfiguration config)
        {
            _config = config;
        }

        public string BalanceApiKey
        {
            get
            {
                string result = _config[STRIPE_BALANCE_API_KEY_SECRET_MANAGER_KEY];
                return result;
            }
        }

        public string BalanceTransacitonsApiKey
        {
            get
            {
                string result = _config[STRIPE_BALANCE_TRANSACTIONS_API_KEY_SECRET_MANAGER_KEY];
                return result;
            }
        }
    }
}
