namespace SimpleBlazorGrid.Core.Tests.Common.Extensions;

public static class FluentAsync
{
    public static async Task<TResult> Do<TSource, TResult>(
        this Task<TSource> @this,
        Func<TSource, Task<TResult>> fn) => await fn(await @this);
        
    public static async Task<TResult> Do<TSource, TResult>(
        this TSource @this,
        Func<TSource, Task<TResult>> fn) => await fn(@this);
 
    public static async Task<TResult> Do<TSource, TResult>(
        this Task<TSource> @this,
        Func<TSource, TResult> fn) => fn(await @this);
}