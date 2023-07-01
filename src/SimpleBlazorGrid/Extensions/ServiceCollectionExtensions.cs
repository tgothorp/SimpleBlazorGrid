using System;
using SimpleBlazorGrid.Configuration;
using SimpleBlazorGrid.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SimpleBlazorGrid.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSimpleBlazorGrid(this IServiceCollection serviceCollection, Action<SimpleDataGridConfiguration> config)
        {
            var defaultOptions = new DefaultConfiguration();
            var configuration = new SimpleDataGridConfiguration();
            
            config.Invoke(configuration);
            configuration.PrimaryColour ??= defaultOptions.PrimaryColour;
            configuration.SecondaryColour ??= defaultOptions.SecondaryColour;
            configuration.CurrencySymbol ??= defaultOptions.CurrencySymbol;
            configuration.LongDateTimeFormat ??= defaultOptions.LongDateTimeFormat;
            configuration.ShortDateTimeFormat ??= defaultOptions.ShortDateTimeFormat;
            configuration.TimeOnlyFormat ??= defaultOptions.TimeOnlyFormat;
            configuration.DefaultDecimalPlaces ??= defaultOptions.DefaultDecimalPlaces.Value;

            serviceCollection.AddSingleton<SimpleDataGridConfiguration>(configuration);
            serviceCollection.AddSingleton<IFormattingService, FormattingService>();

            return serviceCollection;
        }
    }
}