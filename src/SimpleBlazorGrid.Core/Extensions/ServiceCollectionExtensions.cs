using System;
using SimpleBlazorGrid.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleBlazorGrid.Formatting;

namespace SimpleBlazorGrid.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSimpleBlazorGrid(this IServiceCollection serviceCollection)
        {
            var defaultOptions = new DefaultConfiguration();
            
            serviceCollection.AddSingleton<SimpleDataGridConfiguration>(defaultOptions);
            serviceCollection.AddSingleton<SimpleDataGridFormatter>();

            return serviceCollection;
        }
        
        public static IServiceCollection AddSimpleBlazorGrid(this IServiceCollection serviceCollection, Action<SimpleDataGridConfiguration> config)
        {
            var defaultOptions = new DefaultConfiguration();
            var configuration = new SimpleDataGridConfiguration();
            
            config.Invoke(configuration);
            
            configuration.PrimaryColour ??= defaultOptions.PrimaryColour;
            if (!configuration.PrimaryColour.StartsWith("#"))
                configuration.PrimaryColour = $"#{configuration.PrimaryColour}";
            
            configuration.SecondaryColour ??= defaultOptions.SecondaryColour;
            if (!configuration.SecondaryColour.StartsWith("#"))
                configuration.SecondaryColour = $"#{configuration.SecondaryColour}";

            configuration.GlyphColour ??= defaultOptions.GlyphColour;
            if (!configuration.GlyphColour.StartsWith("#"))
                configuration.GlyphColour = $"#{configuration.GlyphColour}";
            
            configuration.CurrencySymbol ??= defaultOptions.CurrencySymbol;
            configuration.LongDateFormat ??= defaultOptions.LongDateFormat;
            configuration.ShortDateFormat ??= defaultOptions.ShortDateFormat;
            configuration.TimeOnlyFormat ??= defaultOptions.TimeOnlyFormat;
            configuration.FullDateTimeFormat ??= defaultOptions.FullDateTimeFormat;
            configuration.DefaultDecimalPlaces ??= defaultOptions.DefaultDecimalPlaces;

            serviceCollection.AddSingleton<SimpleDataGridConfiguration>(configuration);
            serviceCollection.AddSingleton<SimpleDataGridFormatter>();

            return serviceCollection;
        }
    }
}