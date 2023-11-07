using System;

namespace SimpleBlazorGrid.Exceptions;

public class SimpleGridException : Exception
{
    public SimpleGridException(string message) : base(message) { }
}