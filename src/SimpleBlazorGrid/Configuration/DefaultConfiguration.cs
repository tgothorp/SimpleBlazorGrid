using System.Globalization;

namespace SimpleBlazorGrid.Configuration
{
    public class DefaultConfiguration
    {
        public DefaultConfiguration()
        {
            var currentCulture = CultureInfo.CurrentCulture;
            var dateFormatInfo = currentCulture.DateTimeFormat;
            var numberFormatInfo = currentCulture.NumberFormat;

            TimeOnlyFormat = dateFormatInfo.ShortTimePattern;
            ShortDateTimeFormat = dateFormatInfo.ShortDatePattern;
            LongDateTimeFormat = dateFormatInfo.LongDatePattern;

            CurrencySymbol = numberFormatInfo.CurrencySymbol;
            DefaultDecimalPlaces = 4;
        }
        
        public string TimeOnlyFormat { get; set; }
        public string ShortDateTimeFormat { get; set; }
        public string LongDateTimeFormat { get; set; }
        public string CurrencySymbol { get; set; }
        public int? DefaultDecimalPlaces { get; set; }
    }
}