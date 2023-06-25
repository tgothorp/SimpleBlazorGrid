using System;
using AnotherBlazorGrid.Configuration;
using AnotherBlazorGrid.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AnotherBlazorGrid.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSimpleBlazorGrid(this IServiceCollection serviceCollection, Action<SimpleDataGridConfiguration> config)
        {
            var defaultOptions = new DefaultConfiguration();
            var options = new SimpleDataGridConfiguration();
            config.Invoke(options);

            var formattingService = new FormattingService
            {
                CurrencySymbol = options.CurrencySymbol ??= defaultOptions.CurrencySymbol,
                LongDateTimeFormat = options.LongDateTimeFormat ??= defaultOptions.LongDateTimeFormat,
                ShortDateTimeFormat = options.ShortDateTimeFormat ??= defaultOptions.ShortDateTimeFormat,
                TimeOnlyFormat = options.TimeOnlyFormat ??= defaultOptions.TimeOnlyFormat,
                DefaultDecimalPlaces = options.DefaultDecimalPlaces ??= defaultOptions.DefaultDecimalPlaces.Value
            };

            serviceCollection.AddSingleton<FormattingService>(formattingService);

            return serviceCollection;
        }
    }
}