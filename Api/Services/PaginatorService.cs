namespace Api.Services
{
    public class PaginatorService<T> : IPaginatorService<T> where T : class
    {
        public IEnumerable<T> GetPaginated(IEnumerable<T> allRecords, int PageNumber, int PageSize)
        {
            IEnumerable<T> result = allRecords.Skip<T>((PageNumber - 1) * PageSize).Take(PageSize);
            return result;
        }
    }
}
