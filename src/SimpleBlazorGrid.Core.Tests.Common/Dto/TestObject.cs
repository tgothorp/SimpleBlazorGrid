namespace SimpleBlazorGrid.Core.Tests.Common.Dto;

/// <summary>
/// Test object with many different property types
/// </summary>
public class TestObject
{
    // Primitive properties
    public bool BooleanProperty { get; set; }
    public byte ByteProperty { get; set; }

    public char CharProperty { get; set; }
    public string StringProperty { get; set; } = null!;
    
    public short ShortProperty { get; set; }
    public ushort UShortProperty { get; set; }
    public int IntProperty { get; set; }
    public uint UIntProperty { get; set; }
    public long LongProperty { get; set; }
    public ulong ULongProperty { get; set; }

    public double DoubleProperty { get; set; }
    public decimal DecimalProperty { get; set; }
    public float FloatProperty { get; set; }
    
    public DateTime DateTimeProperty { get; set; }
    public DateOnly DateOnlyProperty { get; set; }
    public TimeOnly TimeOnlyProperty { get; set; }
    public TimeSpan TimeSpanProperty { get; set; }
    
    // Nullable properties
    public bool? NullableBooleanProperty { get; set; }
    public byte? NullableByteProperty { get; set; }

    public char? NullableCharProperty { get; set; }
    public string NullableStringProperty { get; set; }
    
    public short? NullableShortProperty { get; set; }
    public ushort? NullableUShortProperty { get; set; }
    public int? NullableIntProperty { get; set; }
    public uint? NullableUIntProperty { get; set; }
    public long? NullableLongProperty { get; set; }
    public ulong? NullableULongProperty { get; set; }

    public double? NullableDoubleProperty { get; set; }
    public decimal? NullableDecimalProperty { get; set; }
    public float? NullableFloatProperty { get; set; }
    
    public DateTime? NullableDateTimeProperty { get; set; }
    public DateOnly? NullableDateOnlyProperty { get; set; }
    public TimeOnly? NullableTimeOnlyProperty { get; set; }
    public TimeSpan? NullableTimeSpanProperty { get; set; }

    public TestObject ShallowClone()
    {
        return (TestObject) MemberwiseClone();
    }
}