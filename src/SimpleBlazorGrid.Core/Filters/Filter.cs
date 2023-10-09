using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using SimpleBlazorGrid.Helpers;

namespace SimpleBlazorGrid.Filters;

public abstract class Filter<T> : ComponentBase
{
    [CascadingParameter]
    public SimpleGrid<T> SimpleGrid { get; set; }

    public Guid Id { get; protected set; } = Guid.Empty;

    /// <summary>
    /// The property the filter is targeting, for example: <code>@(x => x.MyProperty)</code>
    /// </summary>
    [Parameter]
    public Expression<Func<T, object>> For { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string ForHeading { get; set; }

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
    
    // Cache property type
    private Type _propertyType = null;
    public Type PropertyType
    {
        get => _propertyType ?? GetPropertyType();
        protected set => _propertyType = value;
    }

    protected override void OnInitialized()
    {
        if (Id == Guid.Empty)
            Id = Guid.NewGuid();

        SimpleGrid.AddSimpleFilter(this);
        base.OnInitialized();
    }

    private string _headingName = null;
    protected string HeadingName
    {
        get
        {
            if (_headingName is not null)
                return _headingName;

            var columnsForProperty = SimpleGrid.DataGridColumns.Where(x => x.PropertyName == PropertyName).ToArray();
            switch (columnsForProperty.Length)
            {
                case 0:
                    _headingName = PropertyName;
                    break;
                case 1:
                    _headingName = columnsForProperty[0].Heading;
                    break;
                default:
                {
                    var target = SimpleGrid.DataGridColumns.FirstOrDefault(x => x.PropertyName == PropertyName && x.Heading == ForHeading);
                    _headingName = target?.Heading ?? PropertyName;
                    break;
                }
            }

            return _headingName;
        }
    }

    protected string GetPropertyName()
    {
        _propertyName = ExpressionHelper.GetPropertyName(For);
        return _propertyName;
    }

    protected Type GetPropertyType()
    {
        if (For.Body is UnaryExpression unaryExpression)
        {
            _propertyType = unaryExpression.Operand.Type;
            return _propertyType;
        }

        return For.Body.Type;
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