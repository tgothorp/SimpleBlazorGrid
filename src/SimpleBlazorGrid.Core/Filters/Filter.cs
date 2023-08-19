using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SimpleBlazorGrid.Helpers;

namespace SimpleBlazorGrid.Filters
{
    public abstract class Filter<T> : ComponentBase
    {
        [CascadingParameter]
        public SimpleGrid<T> SimpleGrid { get; set; }

        public Guid Id { get; protected set; }

        [Parameter]
        public Expression<Func<T, object>> For { get; set; }

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

        protected override void OnInitialized()
        {
            Id = Guid.NewGuid();
            SimpleGrid.AddSimpleFilter(this);

            base.OnInitialized();
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

            // Reset to first page when applying / removing a filter
            SimpleGrid.PageOptions.CurrentPage = 0;
            await SimpleGrid.ReloadData();
        }

        protected virtual async Task Clear()
        {
            FilterActive = false;
            ShowFilter(false);

            // Reset to first page when applying / removing a filter
            SimpleGrid.PageOptions.CurrentPage = 0;
            await SimpleGrid.ReloadData();
        }
    }
}