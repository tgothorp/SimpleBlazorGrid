namespace SimpleBlazorGrid.Core.Tests.Common.Dto;

public static class TestObjectFactory
{
    public static TestObject CreateOne(bool instantiateNullableProperties = true)
    {
        var faker = TestObjectFaker.Create(instantiateNullableProperties);
        return faker.Generate();
    }

    public static List<TestObject> CreateMany(long? count = null, bool instantiateNullableProperties = true)
    {
        var result = new List<TestObject>();
        count ??= Random.Shared.NextInt64(10, 100);

        for (var i = 0; i < count; i++)
        {
            result.Add(CreateOne(instantiateNullableProperties));
        }

        return result;
    }

    public static List<TestObject> CreateManyIdentical(long? count = null, bool instantiateNullableProperties = true)
    {
        var result = new List<TestObject>();
        count ??= Random.Shared.NextInt64(10, 100);

        var objectToCopy = CreateOne(instantiateNullableProperties);
        result.Add(objectToCopy);

        for (var i = 0; i < count - 1; i++)
        {
            result.Add(objectToCopy.ShallowClone());
        }

        return result;
    }

    public static List<TestObject> CreateManyWithAMixOfNulls(long? count = null)
    {
        var result = new List<TestObject>();
        count ??= Random.Shared.NextInt64(10, 100);

        for (int i = 0; i < count; i++)
        {
            result.Add(CreateOne(i % 2 == 0));
        }

        return result;
    }
}