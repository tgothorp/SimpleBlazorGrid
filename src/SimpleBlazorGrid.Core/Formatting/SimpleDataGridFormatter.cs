using System;
using SimpleBlazorGrid.Configuration;

namespace SimpleBlazorGrid.Formatting
{
    public class SimpleDataGridFormatter
    {
        private readonly SimpleDataGridConfiguration _configuration;

        public SimpleDataGridFormatter(SimpleDataGridConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string FormatProperty(object property, Format format)
        {
            if (property is null)
                return null;

            switch (format)
            {
                case Format.None:
                    return property.ToString();
                case Format.ShortDate:
                    switch (property)
                    {
                        case DateTime dateTime:
                            return dateTime.ToString(_configuration.ShortDateFormat);
                        case DateOnly dateOnly:
                            return dateOnly.ToString(_configuration.ShortDateFormat);
                    }
                    break;
                case Format.LongDate:
                    switch (property)
                    {
                        case DateTime dateTime:
                            return dateTime.ToString(_configuration.LongDateFormat);
                        case DateOnly dateOnly:
                            return dateOnly.ToString(_configuration.LongDateFormat);
                    }
                    break;
                case Format.FullDateTime:
                    switch (property)
                    {
                        case DateTime dateTime:
                            return dateTime.ToString(_configuration.FullDateTimeFormat);
                        case DateOnly dateOnly:
                            return dateOnly.ToString(_configuration.LongDateFormat);
                    }
                    break;
                case Format.Time:
                    switch (property)
                    {
                        case DateTime dateTime:
                            return dateTime.ToString(_configuration.TimeOnlyFormat);
                        case TimeOnly timeOnly:
                            return timeOnly.ToString(_configuration.TimeOnlyFormat);
                    }
                    break;
                case Format.Money:
                    if (property is decimal currency)
                        return $"{_configuration.CurrencySymbol}{currency:n2}";
                    break;
            }

            return property.ToString();
        }
    }
}