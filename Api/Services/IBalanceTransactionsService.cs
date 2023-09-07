using Stripe;

namespace Api.Services
{
    public interface IBalanceTransactionsService
    {
        Task<IEnumerable<BalanceTransaction>> GetAllBalanceTransactions();
    }
}
