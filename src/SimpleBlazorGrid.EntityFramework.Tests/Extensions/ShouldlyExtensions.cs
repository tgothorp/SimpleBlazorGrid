using Shouldly;

namespace SimpleBlazorGrid.EntityFramework.Tests.Extensions;

public static class ShouldlyExtensions
{
    public static void ShouldNotBeNullOrEmpty<T>(this IEnumerable<T> list)
    {
        list.ShouldNotBeNull();
        list.ShouldNotBeEmpty();
    }
}