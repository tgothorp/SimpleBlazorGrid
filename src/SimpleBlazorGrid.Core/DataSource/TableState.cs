using System;
using System.Collections.Generic;
using System.Linq;
using SimpleBlazorGrid.Editing;
using SimpleBlazorGrid.Filters;

namespace SimpleBlazorGrid.DataSource;

public class TableState<T>
{
    public TableState(int itemsPerPage)
    {
        ItemsPerPage = itemsPerPage;
        SelectedItems = new List<T>();
    }

    public T[] Items { get; protected set; } = Array.Empty<T>();
    public List<T> SelectedItems { get; protected set; }

    // Paging
    public int ItemsPerPage { get; protected set; }
    public int TotalItemCount { get; protected set; }
    public int TotalPageCount { get; protected set; }
    public int CurrentPage { get; protected set; }

    // Searching
    public string SearchQuery { get; protected set; }
    public HashSet<string> SearchColumns { get; protected set; } = new();

    // Sorting
    public string SortProperty { get; protected set; }
    public bool SortAscending { get; protected set; }

    // Filtering
    public List<Filter<T>> ActiveFilters { get; protected set; } = new();
    
    // Editing
    public List<EditAction<T>> ItemPropertiesToEdit { get; protected set; } = new();

    public void SetItems(T[] items, int totalItemCount)
    {
        Items = items;

        TotalItemCount = totalItemCount;
        TotalPageCount = (int)Math.Ceiling((decimal)TotalItemCount / ItemsPerPage);
    }

    internal void SelectEverything() => SelectedItems = Items.ToList();
    internal void SelectNothing() => SelectedItems = new List<T>();
    internal void Select(T item) => SelectedItems.Add(item);
    internal void Deselect(T item) => SelectedItems.Remove(item);
    internal bool IsSelected(T item) => SelectedItems.Contains(item);

    internal void UpdateSearchQuery(string searchQuery)
    {
        SearchQuery = searchQuery;
        CurrentPage = 0;
        SelectNothing();
    }

    internal void AddSearchableColumn(string propertyName)
    {
        SearchColumns.Add(propertyName);
    }

    internal void UpdateSortProperty(string sortProperty)
    {
        SortProperty = sortProperty;
        SortAscending = !SortAscending;
        CurrentPage = 0;
        SelectNothing();
    }

    internal void UpdatePagingValues(int totalItemCount)
    {
        TotalItemCount = totalItemCount;
        TotalPageCount = (int)Math.Ceiling((decimal)TotalItemCount / ItemsPerPage);
    }

    internal void AddFilter(Filter<T> filter)
    {
        ActiveFilters.Add(filter);
        CurrentPage = 0;
    }

    internal void RemoveFilter(Filter<T> filter)
    {
        ActiveFilters.Remove(filter);
        CurrentPage = 0;
    }

    internal void ChangePage(int pageIndex)
    {
        CurrentPage = pageIndex;
    }

    internal void UpdateEditActions(List<EditAction<T>> editActions)
    {
        ItemPropertiesToEdit = editActions;
    }

    internal void ClearEditActions()
    {
        ItemPropertiesToEdit = new();
    }
}