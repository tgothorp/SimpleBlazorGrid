namespace AnotherBlazorGrid.Services
{
    public class FormattingService
    {
        public string TimeOnlyFormat { get; set; }
        public string ShortDateTimeFormat { get; set; }
        public string LongDateTimeFormat { get; set; }
        public string CurrencySymbol { get; set; }
        public int DefaultDecimalPlaces { get; set; }
    }
}