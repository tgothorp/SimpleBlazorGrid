using System.Globalization;

namespace SimpleBlazorGrid.Configuration
{
    public class DefaultConfiguration
    {
        public DefaultConfiguration()
        {
            PrimaryColour = "#FF0A54";
            SecondaryColour = "#F7CAD0";
            
            var currentCulture = CultureInfo.CurrentCulture;
            var dateFormatInfo = currentCulture.DateTimeFormat;
            var numberFormatInfo = currentCulture.NumberFormat;

            TimeOnlyFormat = dateFormatInfo.ShortTimePattern;
            ShortDateTimeFormat = dateFormatInfo.ShortDatePattern;
            LongDateTimeFormat = dateFormatInfo.LongDatePattern;

            CurrencySymbol = numberFormatInfo.CurrencySymbol;
            DefaultDecimalPlaces = 4;
        }
        
        public string PrimaryColour { get; set; }
        public string SecondaryColour { get; set; }
        
        public string TimeOnlyFormat { get; set; }
        public string ShortDateTimeFormat { get; set; }
        public string LongDateTimeFormat { get; set; }
        public string CurrencySymbol { get; set; }
        public int? DefaultDecimalPlaces { get; set; }
    }
}