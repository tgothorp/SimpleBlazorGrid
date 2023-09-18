using System;
using SimpleBlazorGrid.Filters;

namespace SimpleBlazorGrid.Exceptions;

public class SimpleGridFilterException<T> : Exception
{
    public Filter<T> Filter { get; }

    public SimpleGridFilterException(Filter<T> filter, string message) : base(message)
    {
        Filter = filter;
    }
}