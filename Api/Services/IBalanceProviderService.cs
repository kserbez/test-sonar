using Stripe;

namespace Api.Services
{
    public interface IBalanceProviderService
    {
        Balance GetBalance();
    }
}
