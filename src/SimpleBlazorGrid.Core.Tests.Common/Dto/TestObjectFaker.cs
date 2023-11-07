using Bogus;

namespace SimpleBlazorGrid.Core.Tests.Common.Dto;

public static class TestObjectFaker
{
    public static Faker<TestObject> Create(bool initNulls = true)
    {
        if (initNulls)
        {
            return new Faker<TestObject>()
                .RuleFor(x => x.BooleanProperty, f => f.Random.Bool())
                .RuleFor(x => x.NullableBooleanProperty, f => f.Random.Bool())
                .RuleFor(x => x.ByteProperty, f => f.Random.Byte())
                .RuleFor(x => x.NullableByteProperty, f => f.Random.Byte())
                .RuleFor(x => x.CharProperty, f => f.Random.Char())
                .RuleFor(x => x.NullableCharProperty, f => f.Random.Char())
                .RuleFor(x => x.StringProperty, f => f.Random.String2(10, 100))
                .RuleFor(x => x.NullableStringProperty, f => f.Random.String2(10, 100))
                .RuleFor(x => x.ShortProperty, f => f.Random.Short())
                .RuleFor(x => x.NullableShortProperty, f => f.Random.Short())
                .RuleFor(x => x.UShortProperty, f => f.Random.UShort())
                .RuleFor(x => x.NullableUShortProperty, f => f.Random.UShort())
                .RuleFor(x => x.IntProperty, f => f.Random.Int())
                .RuleFor(x => x.NullableIntProperty, f => f.Random.Int())
                .RuleFor(x => x.UIntProperty, f => f.Random.UInt())
                .RuleFor(x => x.NullableUIntProperty, f => f.Random.UInt())
                .RuleFor(x => x.LongProperty, f => f.Random.Long())
                .RuleFor(x => x.NullableLongProperty, f => f.Random.Long())
                .RuleFor(x => x.ULongProperty, f => f.Random.ULong())
                .RuleFor(x => x.NullableULongProperty, f => f.Random.ULong())
                .RuleFor(x => x.DoubleProperty, f => f.Random.Double())
                .RuleFor(x => x.NullableDoubleProperty, f => f.Random.Double())
                .RuleFor(x => x.DecimalProperty, f => f.Random.Decimal())
                .RuleFor(x => x.NullableDecimalProperty, f => f.Random.Decimal())
                .RuleFor(x => x.FloatProperty, f => f.Random.Float())
                .RuleFor(x => x.NullableFloatProperty, f => f.Random.Float())
                .RuleFor(x => x.DateTimeProperty, f => f.Date.Recent())
                .RuleFor(x => x.NullableDateTimeProperty, f => f.Date.Recent())
                .RuleFor(x => x.DateOnlyProperty, f => f.Date.RecentDateOnly())
                .RuleFor(x => x.NullableDateOnlyProperty, f => f.Date.RecentDateOnly())
                .RuleFor(x => x.TimeOnlyProperty, f => f.Date.RecentTimeOnly())
                .RuleFor(x => x.NullableTimeOnlyProperty, f => f.Date.RecentTimeOnly())
                .RuleFor(x => x.TimeSpanProperty, f => f.Date.Timespan())
                .RuleFor(x => x.NullableTimeSpanProperty, f => f.Date.Timespan());
        }

        return new Faker<TestObject>()
            .RuleFor(x => x.BooleanProperty, f => f.Random.Bool())
            .RuleFor(x => x.ByteProperty, f => f.Random.Byte())
            .RuleFor(x => x.CharProperty, f => f.Random.Char())
            .RuleFor(x => x.StringProperty, f => f.Random.String2(10, 100))
            .RuleFor(x => x.ShortProperty, f => f.Random.Short())
            .RuleFor(x => x.UShortProperty, f => f.Random.UShort())
            .RuleFor(x => x.IntProperty, f => f.Random.Int())
            .RuleFor(x => x.UIntProperty, f => f.Random.UInt())
            .RuleFor(x => x.LongProperty, f => f.Random.Long())
            .RuleFor(x => x.ULongProperty, f => f.Random.ULong())
            .RuleFor(x => x.DoubleProperty, f => f.Random.Double())
            .RuleFor(x => x.DecimalProperty, f => f.Random.Decimal())
            .RuleFor(x => x.FloatProperty, f => f.Random.Float())
            .RuleFor(x => x.DateTimeProperty, f => f.Date.Recent())
            .RuleFor(x => x.DateOnlyProperty, f => f.Date.RecentDateOnly())
            .RuleFor(x => x.TimeOnlyProperty, f => f.Date.RecentTimeOnly())
            .RuleFor(x => x.TimeSpanProperty, f => f.Date.Timespan());
    }
}