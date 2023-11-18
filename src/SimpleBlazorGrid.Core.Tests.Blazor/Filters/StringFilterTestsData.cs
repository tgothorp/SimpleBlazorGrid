#pragma warning disable CS8603

using System.Collections;
using System.Linq.Expressions;
using SimpleBlazorGrid.Core.Tests.Common.Dto;

namespace SimpleBlazorGrid.Core.Tests.Blazor.Filters;

public static class StringFilterTestsData
{
    public class StringFilterPropertyIsStringData : IEnumerable<object[]>
    {
        Expression<Func<TestObject, object>> _boolean = t => t.BooleanProperty;
        Expression<Func<TestObject, object>> _nullableBoolean = t => t.NullableBooleanProperty;
        Expression<Func<TestObject, object>> _byte = t => t.ByteProperty;
        Expression<Func<TestObject, object>> _nullableByte = t => t.NullableByteProperty;
        Expression<Func<TestObject, object>> _char = t => t.CharProperty;
        Expression<Func<TestObject, object>> _nullableChar = t => t.NullableCharProperty;
        Expression<Func<TestObject, object>> _string = t => t.StringProperty;
        Expression<Func<TestObject, object>> _nullableString = t => t.NullableStringProperty;
        Expression<Func<TestObject, object>> _short = t => t.ShortProperty;
        Expression<Func<TestObject, object>> _nullableShort = t => t.NullableShortProperty;
        Expression<Func<TestObject, object>> _ushort = t => t.UShortProperty;
        Expression<Func<TestObject, object>> _nullableUshort = t => t.NullableUShortProperty;
        Expression<Func<TestObject, object>> _int = t => t.IntProperty;
        Expression<Func<TestObject, object>> _nullableInt = t => t.NullableIntProperty;
        
        
        Expression<Func<TestObject, object>> _decimal = t => t.DecimalProperty;
        Expression<Func<TestObject, object>> _nullableDecimal = t => t.NullableDecimalProperty;

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { _boolean, true };
            yield return new object[] { _nullableBoolean, true };
            yield return new object[] { _byte, true };
            yield return new object[] { _nullableByte, true };
            yield return new object[] { _char, true };
            yield return new object[] { _nullableChar, true };
            yield return new object[] { _string, false };
            yield return new object[] { _nullableString, false };
            yield return new object[] { _short, true };
            yield return new object[] { _nullableShort, true };
            yield return new object[] { _ushort, true };
            yield return new object[] { _nullableUshort, true };
            yield return new object[] { _int, true };
            yield return new object[] { _nullableInt, true };
            yield return new object[] { _decimal, true };
            yield return new object[] { _nullableDecimal, true };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}