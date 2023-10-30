﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Domain;
using SimpleBlazorGrid.EntityFramework.Sandbox.Core.Infrastructure;
using SimpleBlazorGrid.EntityFramework.Tests.Setup;

namespace SimpleBlazorGrid.EntityFramework.Tests.Filters;

[SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]
public class StringFilterTests
{
    [Fact]
    public async Task StringFilter_IgnoreCase_SameCaseMatches()
    {
        var customer = await DatabaseSetup.AddCustomer();
        var context = DatabaseSetup.CreateDatabaseContext();

        var filter = new SimpleStringFilter<Customer>
        {
            For = x => x.CustomerId,
            IgnoreCase = true,
            Value = customer.CustomerId
        };

        var source = TestSetup.CreateDataSource(context.Customers, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeEmpty();
        result.SingleOrDefault(x => x.CustomerId == customer.CustomerId).ShouldNotBeNull();
    }

    [Fact]
    public async Task StringFilter_IgnoreCase_DifferentCaseMatches()
    {
        var customer = await DatabaseSetup.AddCustomer();
        var context = DatabaseSetup.CreateDatabaseContext();

        var filter = new SimpleStringFilter<Customer>
        {
            For = x => x.CustomerId,
            IgnoreCase = true,
            Value = customer.CustomerId.ToUpper()
        };

        var source = TestSetup.CreateDataSource(context.Customers, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeEmpty();
        result.SingleOrDefault(x => x.CustomerId == customer.CustomerId).ShouldNotBeNull();
    }
    
    [Fact]
    public async Task StringFilter_MatchCase_SameCaseMatches()
    {
        var customer = await DatabaseSetup.AddCustomer();
        var context = DatabaseSetup.CreateDatabaseContext();

        var filter = new SimpleStringFilter<Customer>
        {
            For = x => x.CustomerId,
            IgnoreCase = false,
            Value = customer.CustomerId
        };

        var source = TestSetup.CreateDataSource(context.Customers, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeEmpty();
        result.SingleOrDefault(x => x.CustomerId == customer.CustomerId).ShouldNotBeNull();
    }

    [Fact]
    public async Task StringFilter_MatchCase_DifferentCaseDoesNotMatch()
    {
        var customer = await DatabaseSetup.AddCustomer();
        var context = DatabaseSetup.CreateDatabaseContext();

        var filter = new SimpleStringFilter<Customer>
        {
            For = x => x.CustomerId,
            IgnoreCase = false,
            Value = customer.CustomerId.ToUpper()
        };

        var source = TestSetup.CreateDataSource(context.Customers, filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldBeEmpty();
    }

    [Fact]
    public async Task StringFilter_NestedProperty_IgnoreCase_SameCaseMatches()
    {
        var orderAndCustomer = await DatabaseSetup.AddOrder();
        var context = DatabaseSetup.CreateDatabaseContext();

        var filter = new SimpleStringFilter<Order>()
        {
            For = x => x.Customer.ContactName,
            IgnoreCase = true,
            Value = orderAndCustomer.Customer.ContactName
        };

        var source = TestSetup.CreateDataSource(context.Orders.Include(x => x.Customer), filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeEmpty();
        result.SingleOrDefault(x => x.CustomerId == orderAndCustomer.CustomerId).ShouldNotBeNull();
    }

    [Fact]
    public async Task StringFilter_NonExact_MatchesWithPartialValue()
    {
        var orderAndCustomer = await DatabaseSetup.AddOrder();
        var context = DatabaseSetup.CreateDatabaseContext();

        var filter = new SimpleStringFilter<Order>()
        {
            For = x => x.Customer.ContactName,
            Exact = false,
            Value = $"{orderAndCustomer.Customer.ContactName[0]}{orderAndCustomer.Customer.ContactName[1]}"
        };

        var source = TestSetup.CreateDataSource(context.Orders.Include(x => x.Customer), filter);
        var result = await source.Items(CancellationToken.None);
        result.ShouldNotBeEmpty();
        result.SingleOrDefault(x => x.CustomerId == orderAndCustomer.CustomerId).ShouldNotBeNull();
    }
}