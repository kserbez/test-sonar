using Stripe;

namespace Api.Services
{
    public interface IPaginatorService<T> where T : class
    {
        IEnumerable<T> GetPaginated(IEnumerable<T> allRecords, int PageNumber, int PageSize);
    }
}
