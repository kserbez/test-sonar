using Stripe;

namespace Api.Services
{
    public interface IBalanceTransactionsService
    {
        IEnumerable<BalanceTransaction> GetAllBalanceTransactions();
    }
}
