using Stripe;

namespace Api.Services
{
    public interface IBalanceProviderService
    {
        Task<Balance> GetBalance();
    }
}
