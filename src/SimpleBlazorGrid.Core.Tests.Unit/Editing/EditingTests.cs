using System.Collections;
using System.Linq.Expressions;
using SimpleBlazorGrid.Core.Tests.Common.Dto;
using SimpleBlazorGrid.Editing;
using SimpleBlazorGrid.Helpers;

namespace SimpleBlazorGrid.Core.Tests.Unit.Editing;

public class EditingTests
{
    [Theory]
    [ClassData(typeof(TargetPropertyTypeSupportedDoesNotThrowClassData))]
    public void Editing_TargetPropertyTypeSupported_DoesNotThrow(Expression<Func<TestObject, object>> expression, object newValue)
    {
        // Arrange
        var testObject = TestObjectFactory.CreateOne(instantiateNullableProperties: false);
        var editAction = new EditAction<TestObject>(expression, newValue);
        
        // Act
        editAction.Apply(ref testObject);
        
        // Assert
        var propertyInfo = ExpressionHelper.GetPropertyInfo(expression);
        var setNewValue = propertyInfo.GetValue(testObject);
        setNewValue.ShouldBe(newValue);
    }
    
    public class TargetPropertyTypeSupportedDoesNotThrowClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { TestObjectExpressions.Boolean, true };
            yield return new object[] { TestObjectExpressions.Boolean, false };
            yield return new object[] { TestObjectExpressions.NullableBoolean, true };
            yield return new object[] { TestObjectExpressions.NullableBoolean, false };
            yield return new object[] { TestObjectExpressions.NullableBoolean, null! };
            yield return new object[] { TestObjectExpressions.String, null! };
            yield return new object[] { TestObjectExpressions.String, "Test" };
            yield return new object[] { TestObjectExpressions.String, "" };
            yield return new object[] { TestObjectExpressions.NullableString, null! };
            yield return new object[] { TestObjectExpressions.NullableString, "Test" };
            yield return new object[] { TestObjectExpressions.NullableString, "" };
            yield return new object[] { TestObjectExpressions.Short, short.MinValue };
            yield return new object[] { TestObjectExpressions.Short, short.MaxValue };
            yield return new object[] { TestObjectExpressions.NullableShort, short.MaxValue };
            yield return new object[] { TestObjectExpressions.NullableShort, short.MaxValue };
            yield return new object[] { TestObjectExpressions.NullableShort, null! };
            yield return new object[] { TestObjectExpressions.DateOnly, DateOnly.MinValue };
            yield return new object[] { TestObjectExpressions.DateOnly, DateOnly.FromDateTime(DateTime.Today) };
            yield return new object[] { TestObjectExpressions.NullableDateOnly, null! };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}