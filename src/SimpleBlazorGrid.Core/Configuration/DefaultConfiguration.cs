using System.Globalization;

namespace SimpleBlazorGrid.Configuration
{
    public class DefaultConfiguration : SimpleDataGridConfiguration
    {
        public DefaultConfiguration()
        {
            PrimaryColour = "#FF0A54";
            SecondaryColour = "#F7CAD0";
            GlyphColour = "#000000";
            
            var currentCulture = CultureInfo.CurrentCulture;
            var dateFormatInfo = currentCulture.DateTimeFormat;
            var numberFormatInfo = currentCulture.NumberFormat;

            TimeOnlyFormat = dateFormatInfo.ShortTimePattern;
            ShortDateTimeFormat = dateFormatInfo.ShortDatePattern;
            LongDateTimeFormat = dateFormatInfo.LongDatePattern;

            CurrencySymbol = numberFormatInfo.CurrencySymbol;
            DefaultDecimalPlaces = 4;
        }
    }
}