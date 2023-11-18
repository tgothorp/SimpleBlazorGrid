using System.Linq.Expressions;

namespace SimpleBlazorGrid.Core.Tests.Common.Dto;

public static class TestObjectExpressions
{
    public static Expression<Func<TestObject, object>> Boolean => x => x.BooleanProperty;
    public static Expression<Func<TestObject, object>> NullableBoolean => x => x.NullableBooleanProperty!;
    public static Expression<Func<TestObject, object>> Byte => x => x.ByteProperty;
    public static Expression<Func<TestObject, object>> NullableByte => x => x.NullableByteProperty!;
    public static Expression<Func<TestObject, object>> Char => x => x.CharProperty;
    public static Expression<Func<TestObject, object>> NullableChar => x => x.NullableCharProperty!;
    public static Expression<Func<TestObject, object>> String => x => x.StringProperty;
    public static Expression<Func<TestObject, object>> NullableString => x => x.NullableStringProperty!;
    public static Expression<Func<TestObject, object>> Short => x => x.ShortProperty;
    public static Expression<Func<TestObject, object>> NullableShort => x => x.NullableShortProperty!;
    public static Expression<Func<TestObject, object>> UShort => x => x.UShortProperty;
    public static Expression<Func<TestObject, object>> NullableUShort => x => x.NullableUShortProperty!;
    public static Expression<Func<TestObject, object>> Int => x => x.IntProperty;
    public static Expression<Func<TestObject, object>> NullableInt => x => x.NullableIntProperty!;
    public static Expression<Func<TestObject, object>> UInt => x => x.UIntProperty;
    public static Expression<Func<TestObject, object>> NullableUInt => x => x.NullableUIntProperty!;
    public static Expression<Func<TestObject, object>> Long => x => x.LongProperty;
    public static Expression<Func<TestObject, object>> NullableLong => x => x.NullableLongProperty!;
    public static Expression<Func<TestObject, object>> ULong => x => x.ULongProperty;
    public static Expression<Func<TestObject, object>> NullableULong => x => x.NullableULongProperty!;
    public static Expression<Func<TestObject, object>> Double => x => x.DoubleProperty;
    public static Expression<Func<TestObject, object>> NullableDouble => x => x.NullableDoubleProperty!;
    public static Expression<Func<TestObject, object>> Decimal => x => x.DecimalProperty;
    public static Expression<Func<TestObject, object>> NullableDecimal => x => x.NullableDecimalProperty!;
    public static Expression<Func<TestObject, object>> Float => x => x.FloatProperty;
    public static Expression<Func<TestObject, object>> NullableFloat => x => x.NullableFloatProperty!;
    public static Expression<Func<TestObject, object>> DateTime => x => x.DateTimeProperty;
    public static Expression<Func<TestObject, object>> NullableDateTime => x => x.NullableDateTimeProperty!;
    public static Expression<Func<TestObject, object>> DateOnly => x => x.DateOnlyProperty;
    public static Expression<Func<TestObject, object>> NullableDateOnly => x => x.NullableDateOnlyProperty!;
    public static Expression<Func<TestObject, object>> TimeOnly => x => x.TimeOnlyProperty;
    public static Expression<Func<TestObject, object>> NullableTimeOnly => x => x.NullableTimeOnlyProperty!;
    public static Expression<Func<TestObject, object>> TimeSpan => x => x.TimeSpanProperty;
    public static Expression<Func<TestObject, object>> NullableTimeSpan => x => x.NullableTimeSpanProperty!;
}