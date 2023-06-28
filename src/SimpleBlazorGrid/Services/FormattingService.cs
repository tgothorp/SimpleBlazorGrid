using System;
using SimpleBlazorGrid.Configuration;
using SimpleBlazorGrid.Enums;

namespace SimpleBlazorGrid.Services
{
    public interface IFormattingService
    {
        public string FormatProperty(object property, Format format);
    }
    
    public class FormattingService : IFormattingService
    {
        private readonly SimpleDataGridConfiguration _configuration;

        public FormattingService(SimpleDataGridConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string FormatProperty(object property, Format format)
        {
            switch (format)
            {
                case Format.None:
                    return property.ToString();
                case Format.ShortDate:
                    if (property is DateTime date)
                        return date.ToString(_configuration.ShortDateTimeFormat);
                    break;
                case Format.LongDate:
                    break;
                case Format.Time:
                    break;
                case Format.Money:
                    if (property is decimal currency)
                        return $"{_configuration.CurrencySymbol}{currency:n2}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }

            return property.ToString();
        }
    }
}