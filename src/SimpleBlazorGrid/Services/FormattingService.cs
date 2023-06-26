using System;
using SimpleBlazorGrid.Enums;

namespace SimpleBlazorGrid.Services
{
    public class FormattingService
    {
        public string TimeOnlyFormat { get; set; }
        public string ShortDateTimeFormat { get; set; }
        public string LongDateTimeFormat { get; set; }
        public string CurrencySymbol { get; set; }
        public int DefaultDecimalPlaces { get; set; }

        public string FormatProperty(object property, Format format)
        {
            switch (format)
            {
                case Format.None:
                    return property.ToString();
                case Format.ShortDate:
                    break;
                case Format.LongDate:
                    break;
                case Format.Time:
                    break;
                case Format.Money:
                    if (property is decimal currency)
                        return currency.ToString("C");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }

            return property.ToString();
        }
    }
}