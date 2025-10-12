using CurrencyServices.CurrencyFetcher.Application.Interfaces;
using CurrencyServices.CurrencyFetcher.Infrastructure.Interfaces;
using CurrencyServices.CurrencyFetcher.Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CurrencyServices.CurrencyFetcher.Infrastructure.Services;

public class CurrencyFetcherBackgroundService : ICurrencyFetcherBackgroundService
{
    private readonly BackgroundServiceOptions _backgroundServiceOptions;
    private readonly IServiceProvider _serviceProvider;

    public CurrencyFetcherBackgroundService(IOptions<BackgroundServiceOptions> options,
        IServiceProvider serviceProvider)
    {
        _backgroundServiceOptions = options.Value;
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var currencyService = scope.ServiceProvider.GetRequiredService<ICurrencyService>();

                    var result = await currencyService.FetchCurrenciesAndSave();
                }
            }
            catch (Exception ex)
            {

            }

            await Task.Delay(TimeSpan.FromMinutes(_backgroundServiceOptions.DelayInMinutes));
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
