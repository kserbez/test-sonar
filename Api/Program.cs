using Api.Middleware;
using Api.Services;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IStripeConfigurationService, StripeConfigurationService>();
builder.Services.AddScoped <IBalanceProviderService, BalanceProviderService>();
builder.Services.AddScoped <IBalanceTransactionsService, BalanceTransactionsService>();
builder.Services.AddScoped<IPaginatorService<BalanceAmount>, PaginatorService<BalanceAmount>>();
builder.Services.AddScoped<IPaginatorService<BalanceTransaction>, PaginatorService<BalanceTransaction>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseMiddleware<ExceptionMiddleware>();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
