using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SimpleBlazorGrid.Helpers;
using SimpleBlazorGrid.Interfaces;
using SimpleBlazorGrid.Options;
using SimpleBlazorGrid.Options.Filters;

namespace SimpleBlazorGrid.Filters
{
    public abstract class Filter<T> : ComponentBase
    {
        [CascadingParameter]
        public IDataGrid<T> DataGrid { get; set; }
        
        public Guid Id { get; protected set; }

        [Parameter]
        public Expression<Func<T, object>> For { get; set; }
        public abstract Expression<Func<T, bool>> ApplyFilter();

        public bool FilterActive { get; private set; }

        protected bool ShouldShowFilter { get; private set; }
        public void ShowFilter(bool show) => ShouldShowFilter = show;

        // Cache property name
        private string _propertyName = null;
        public string PropertyName
        {
            get => _propertyName ?? GetPropertyName();
            protected set => _propertyName = value;
        }

        protected string GetPropertyName()
        {
            _propertyName = ExpressionHelper.GetPropertyName(For);
            return _propertyName;
        }

        protected virtual async Task Apply()
        {
            FilterActive = true;
            ShowFilter(false);
            await DataGrid.ReloadData();
        }

        protected virtual async Task Clear()
        {
            FilterActive = false;
            ShowFilter(false);
            await DataGrid.ReloadData();
        }
    }
}