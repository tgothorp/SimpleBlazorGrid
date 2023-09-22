using System;
using Microsoft.AspNetCore.Components.Web;

// ReSharper disable once CheckNamespace
namespace SimpleBlazorGrid.Internal;

public class DataGridErrorBoundary : ErrorBoundary
{
    public Exception Exception => base.CurrentException;
}