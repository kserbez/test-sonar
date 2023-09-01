namespace Api.Services
{
    public interface IStripeConfigurationService
    {
        string BalanceApiKey { get; }

        string BalanceTransacitonsApiKey { get; }

    }
}
