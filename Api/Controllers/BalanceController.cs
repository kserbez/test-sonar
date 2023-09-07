using Api.DTO;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Api.Controllers;

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
    public async Task<IActionResult> GetAvailableBalanceAmounts([FromQuery] PaginationDTO paginationParameters)
    {
        Balance balance = await _balanceProviderService.GetBalance();
        List<BalanceAmount> result = _balanceAmountPaginatorService.GetPaginated(balance.Available,
            paginationParameters.PageNumber, paginationParameters.PageSize).ToList();

        return Ok(result);    
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> GetBalanceTransactions([FromQuery] PaginationDTO paginationParameters)
    {
        IEnumerable<BalanceTransaction> balanceTransactions = await _balanceTransactionsService.GetAllBalanceTransactions();
        List<BalanceTransaction> result = _balanceTransactionPaginatorService.GetPaginated(balanceTransactions,
            paginationParameters.PageNumber, paginationParameters.PageSize).ToList();

        return Ok(result);
    }

}
