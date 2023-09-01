using Api.DTO;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.ComponentModel.DataAnnotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("balance")]
    public class BalanceController : ControllerBase
    {
        private readonly IBalanceProviderService _balanceProviderService;
        private readonly IBalanceTransactionsService _balanceTransactionsService;
        private readonly IPaginatorService<BalanceAmount> _balanceAmountPaginatorService;
        private readonly IPaginatorService<BalanceTransaction> _balanceTransactionPaginatorService;

        public BalanceController(IBalanceProviderService balanceProviderService, IBalanceTransactionsService balanceTransactionsService,
            IPaginatorService<BalanceAmount> balanceAmountPaginatorService, IPaginatorService<BalanceTransaction> balanceTransactionPaginatorService)
        {
            _balanceProviderService = balanceProviderService;
            _balanceTransactionsService = balanceTransactionsService;
            _balanceAmountPaginatorService = balanceAmountPaginatorService;
            _balanceTransactionPaginatorService = balanceTransactionPaginatorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BalanceAmount>> GetAvailableBalanceAmounts([FromQuery] PaginationDTO paginationParameters)
        {
            Balance balance = _balanceProviderService.GetBalance();
            List<BalanceAmount> paginatedBalanceAmonts = _balanceAmountPaginatorService.GetPaginated(balance.Available,
                paginationParameters.PageNumber, paginationParameters.PageSize).ToList();

            return paginatedBalanceAmonts;    
        }

        [HttpGet("transactions")]
        public ActionResult<IEnumerable<BalanceTransaction>> GetBalanceTransactions([FromQuery] PaginationDTO paginationParameters)
        {
            IEnumerable<BalanceTransaction> balanceTransactions = _balanceTransactionsService.GetAllBalanceTransactions();
            List<BalanceTransaction> result = _balanceTransactionPaginatorService.GetPaginated(balanceTransactions,
                paginationParameters.PageNumber, paginationParameters.PageSize).ToList();

            return result;
        }

    }
}