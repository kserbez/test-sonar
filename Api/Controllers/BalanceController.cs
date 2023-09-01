using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Api.Controllers
{
    [ApiController]
    [Route("balance")]
    public class BalanceController : ControllerBase
    {

        private readonly IStripeConfigurationService _stripeConfigurationService;

        public BalanceController(IStripeConfigurationService stripeConfigurationService)
        {
            _stripeConfigurationService = stripeConfigurationService;
        }

        [HttpGet]
        public ActionResult<Balance> GetBalance()
        {
            try
            {
                string balanceApiKey = _stripeConfigurationService.BalanceApiKey;
                StripeConfiguration.ApiKey = balanceApiKey;

                var service = new BalanceService();
                Balance balance = service.Get();

                return balance;
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }
    }
}