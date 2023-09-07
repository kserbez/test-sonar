using Api.Controllers;
using Api.DTO;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Stripe;
using System.Collections.Generic;
using System.Text.Json;

namespace Tests.Controllers;

public class BalanceControllerTest
{
    private Mock<IBalanceProviderService> balanceProviderService;
    private Mock<IBalanceTransactionsService> balanceTransactionsService;
    private Mock<IPaginatorService<BalanceAmount>> balanceAmountPaginatorService;
    private Mock<IPaginatorService<BalanceTransaction>> balanceTransactionPaginatorService;

    public BalanceControllerTest()
    {
        balanceProviderService = new Mock<IBalanceProviderService>();
        balanceTransactionsService = new Mock<IBalanceTransactionsService>();
        balanceAmountPaginatorService = new Mock<IPaginatorService<BalanceAmount>>();
        balanceTransactionPaginatorService = new Mock<IPaginatorService<BalanceTransaction>>();
    }


    [Fact]
    public async void GetAvailableBalanceAmounts_ReturnsSameBalanceAmounts()
    {
        // Arrange
        Balance balance = GetBalanceMockData();
        IEnumerable<BalanceAmount> balanceAmounts = balance.Available;
        PaginationDTO mockPaginationDTO = new PaginationDTO() { PageNumber = 1, PageSize = 10 };

        balanceProviderService.Setup(x => x.GetBalance()).ReturnsAsync(balance);
        balanceAmountPaginatorService.Setup(x => x.GetPaginated(balanceAmounts,
                mockPaginationDTO.PageNumber, mockPaginationDTO.PageSize)).Returns(balanceAmounts);

        var controller = new BalanceController(balanceProviderService.Object, balanceTransactionsService.Object,
            balanceAmountPaginatorService.Object, balanceTransactionPaginatorService.Object);

        // Act
        IActionResult actionResult = await controller.GetAvailableBalanceAmounts(mockPaginationDTO);
        
        // Assert
        Assert.IsType<OkObjectResult>(actionResult);
        OkObjectResult? okActionResult = actionResult as OkObjectResult;
        Assert.NotNull(okActionResult);
        IEnumerable<BalanceAmount>? actualValue = okActionResult.Value as IEnumerable<BalanceAmount>;
        Assert.NotNull(actualValue);
        Assert.Equal(balanceAmounts.Count(), actualValue.Count());
        Assert.Equal(balanceAmounts, actualValue);
    }

    [Fact]
    public async void GetBalanceTransactions_ReturnsSameTransactions()
    {
        // Arrange
        IEnumerable<BalanceTransaction> transactions = GetBalanceTransactionsMockData();
        PaginationDTO mockPaginationDTO = new PaginationDTO() { PageNumber = 1, PageSize = 10 };

        balanceTransactionsService.Setup(x => x.GetAllBalanceTransactions()).ReturnsAsync(transactions);
        balanceTransactionPaginatorService.Setup(x => x.GetPaginated(transactions, 
            mockPaginationDTO.PageNumber, mockPaginationDTO.PageSize)).Returns(transactions);

        var controller = new BalanceController(balanceProviderService.Object, balanceTransactionsService.Object,
            balanceAmountPaginatorService.Object, balanceTransactionPaginatorService.Object);

        // Act
        IActionResult actionResult = await controller.GetBalanceTransactions(mockPaginationDTO);

        // Assert
        Assert.IsType<OkObjectResult>(actionResult);
        OkObjectResult? okActionResult = actionResult as OkObjectResult;
        IEnumerable<BalanceTransaction>? actualValue = okActionResult.Value as IEnumerable<BalanceTransaction>;
        Assert.NotNull(actualValue);
        Assert.Equal(transactions.Count(), actualValue.Count());
        Assert.Equal(transactions, actualValue);
    }


    private Balance GetBalanceMockData()
    {
        var jsonString = "[ { \"amount\": 2217713, \"currency\": \"cad\", \"sourceTypes\": { \"bank_account\": 0, \"bitcoin_receiver\": 0, \"card\": 2217713 }, \"rawJObject\": { \"amount\": [], \"currency\": [], \"source_types\": [ [ [] ], [ [] ], [ [] ] ] }, \"stripeResponse\": null }, { \"amount\": 2685, \"currency\": \"nok\", \"sourceTypes\": { \"bank_account\": 0, \"bitcoin_receiver\": 0, \"card\": 2685 }, \"rawJObject\": { \"amount\": [], \"currency\": [], \"source_types\": [ [ [] ], [ [] ], [ [] ] ] }, \"stripeResponse\": null }, { \"amount\": 7254790, \"currency\": \"gbp\", \"sourceTypes\": { \"bank_account\": 0, \"bitcoin_receiver\": 0, \"card\": 7254790 }, \"rawJObject\": { \"amount\": [], \"currency\": [], \"source_types\": [ [ [] ], [ [] ], [ [] ] ] }, \"stripeResponse\": null } ]";
        var balanceAmounts = JsonSerializer.Deserialize<List<BalanceAmount>>(jsonString);

        Balance result = new Balance
        {
            Available = balanceAmounts
        };

        return result;
    }
    
    private IEnumerable<BalanceTransaction> GetBalanceTransactionsMockData()
    {
        var jsonString = "[{\"id\": \"txn_3NnMD52eZvKYlo2C0oQ1dxPv\", \"object\": \"balance_transaction\", \"amount\": 10125, \"availableOn\": \"2023-09-13T00:00:00Z\", \"created\": \"2023-09-06T13:56:01Z\", \"currency\": \"usd\", \"description\": \"fdle test\", \"exchangeRate\": null, \"fee\": 324, \"feeDetails\": [ { \"amount\": 324, \"application\": null, \"currency\": \"usd\", \"description\": \"Stripe processing fees\", \"type\": \"stripe_fee\", \"rawJObject\": { \"amount\": [], \"application\": [], \"currency\": [], \"description\": [], \"type\": [] }, \"stripeResponse\": null } ], \"net\": 9801, \"reportingCategory\": \"charge\", \"sourceId\": \"ch_3NnMD52eZvKYlo2C0uDXqgbb\", \"source\": null, \"status\": \"pending\", \"type\": \"charge\", \"rawJObject\": { \"id\": [], \"object\": [], \"amount\": [], \"available_on\": [], \"created\": [], \"currency\": [], \"description\": [], \"exchange_rate\": [], \"fee\": [], \"fee_details\": [ [ [ [] ], [ [] ], [ [] ], [ [] ], [ [] ] ] ], \"net\": [], \"reporting_category\": [], \"source\": [], \"status\": [], \"type\": [] }, \"stripeResponse\": null\r\n },\r\n { \"id\": \"txn_3NnMB32eZvKYlo2C0CcpaaMt\", \"object\": \"balance_transaction\", \"amount\": 62195, \"availableOn\": \"2023-09-13T00:00:00Z\", \"created\": \"2023-09-06T13:54:27Z\", \"currency\": \"usd\", \"description\": null, \"exchangeRate\": 1.2439, \"fee\": 1834, \"feeDetails\": [ { \"amount\": 1834, \"application\": null, \"currency\": \"usd\", \"description\": \"Stripe processing fees\", \"type\": \"stripe_fee\", \"rawJObject\": { \"amount\": [], \"application\": [], \"currency\": [], \"description\": [], \"type\": [] }, \"stripeResponse\": null } ], \"net\": 60361, \"reportingCategory\": \"charge\", \"sourceId\": \"ch_3NnMB32eZvKYlo2C0HtSYMpY\", \"source\": null, \"status\": \"pending\", \"type\": \"charge\", \"rawJObject\": { \"id\": [], \"object\": [], \"amount\": [], \"available_on\": [], \"created\": [], \"currency\": [], \"description\": [], \"exchange_rate\": [], \"fee\": [], \"fee_details\": [ [ [ [] ], [ [] ], [ [] ], [ [] ], [ [] ] ] ], \"net\": [], \"reporting_category\": [], \"source\": [], \"status\": [], \"type\": [] }, \"stripeResponse\": null\r\n },\r\n { \"id\": \"txn_3NnMAs2eZvKYlo2C1QpjzcG2\", \"object\": \"balance_transaction\", \"amount\": 50, \"availableOn\": \"2023-09-13T00:00:00Z\", \"created\": \"2023-09-06T13:53:42Z\", \"currency\": \"usd\", \"description\": \"TestDescription\", \"exchangeRate\": null, \"fee\": 31, \"feeDetails\": [ { \"amount\": 31, \"application\": null, \"currency\": \"usd\", \"description\": \"Stripe processing fees\", \"type\": \"stripe_fee\", \"rawJObject\": { \"amount\": [], \"application\": [], \"currency\": [], \"description\": [], \"type\": [] }, \"stripeResponse\": null } ], \"net\": 19, \"reportingCategory\": \"charge\", \"sourceId\": \"ch_3NnMAs2eZvKYlo2C1YwKp4Tt\", \"source\": null, \"status\": \"pending\", \"type\": \"charge\", \"rawJObject\": { \"id\": [], \"object\": [], \"amount\": [], \"available_on\": [], \"created\": [], \"currency\": [], \"description\": [], \"exchange_rate\": [], \"fee\": [], \"fee_details\": [ [ [ [] ], [ [] ], [ [] ], [ [] ], [ [] ] ] ], \"net\": [], \"reporting_category\": [], \"source\": [], \"status\": [], \"type\": [] }, \"stripeResponse\": null } ]";
        var result = JsonSerializer.Deserialize<List<BalanceTransaction>>(jsonString);

        return result;
    }
}