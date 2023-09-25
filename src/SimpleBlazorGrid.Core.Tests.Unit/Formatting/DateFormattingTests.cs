using SimpleBlazorGrid.Configuration;
using SimpleBlazorGrid.Formatting;

namespace SimpleBlazorGrid.Core.Tests.Unit.Formatting;

public class DateFormattingTests
{
    private DateTime _dateTime = new(2023, 09, 25, 13, 08, 51);
    private DateOnly _dateOnly = new DateOnly(2023, 09, 25);
    private TimeOnly _timeOnly = new TimeOnly(13, 08, 51);
    
    [Fact]
    public void FullDateTime_DateTime()
    {
        var config = new SimpleDataGridConfiguration {FullDateTimeFormat = "dd MMMM yyyy HH:mm:ss"};
        var formatter = new SimpleDataGridFormatter(config);
        var result = formatter.FormatProperty(_dateTime, Format.FullDateTime);

        result.ShouldNotBeNull();
        result.ShouldBe("25 September 2023 13:08:51");
    }
    
    [Fact]
    public void FullDateTime_DateOnly()
    {
        var config = new SimpleDataGridConfiguration {LongDateFormat = "dd MMMM yyyy"};
        var formatter = new SimpleDataGridFormatter(config);
        var result = formatter.FormatProperty(_dateOnly, Format.FullDateTime);

        result.ShouldNotBeNull();
        result.ShouldBe("25 September 2023");
    }

    [Fact]
    public void ShortDate_DateTime()
    {
        var config = new SimpleDataGridConfiguration {ShortDateFormat = "dd/MM/yy"};
        var formatter = new SimpleDataGridFormatter(config);
        var result = formatter.FormatProperty(_dateTime, Format.ShortDate);

        result.ShouldNotBeNull();
        result.ShouldBe("25/09/23");
    }

    [Fact]
    public void ShortDate_DateOnly()
    {
        var config = new SimpleDataGridConfiguration {ShortDateFormat = "dd/MM/yy"};
        var formatter = new SimpleDataGridFormatter(config);
        var result = formatter.FormatProperty(_dateOnly, Format.ShortDate);

        result.ShouldNotBeNull();
        result.ShouldBe("25/09/23");
    }

    [Fact]
    public void LongDate_DateTime()
    {
        var config = new SimpleDataGridConfiguration {LongDateFormat = "dd MMMM yyyy"};
        var formatter = new SimpleDataGridFormatter(config);
        var result = formatter.FormatProperty(_dateTime, Format.LongDate);
        
        result.ShouldNotBeNull();
        result.ShouldBe("25 September 2023");
    }

    [Fact]
    public void LongDate_DateOnly()
    {
        var config = new SimpleDataGridConfiguration {LongDateFormat = "dd MMMM yyyy"};
        var formatter = new SimpleDataGridFormatter(config);
        var result = formatter.FormatProperty(_dateOnly, Format.LongDate);

        result.ShouldNotBeNull();
        result.ShouldBe("25 September 2023");
    }
    
    [Fact]
    public void Time_DateTime()
    {
        var config = new SimpleDataGridConfiguration {TimeOnlyFormat = "HH:mm:ss"};
        var formatter = new SimpleDataGridFormatter(config);
        var result = formatter.FormatProperty(_dateTime, Format.Time);
        
        result.ShouldNotBeNull();
        result.ShouldBe("13:08:51");
    }

    [Fact]
    public void Time_TimeOnly()
    {
        var config = new SimpleDataGridConfiguration {TimeOnlyFormat = "HH:mm:ss"};
        var formatter = new SimpleDataGridFormatter(config);
        var result = formatter.FormatProperty(_timeOnly, Format.Time);

        result.ShouldNotBeNull();
        result.ShouldBe("13:08:51");
    }
}