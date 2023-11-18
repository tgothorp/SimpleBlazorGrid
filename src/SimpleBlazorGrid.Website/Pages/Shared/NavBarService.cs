namespace SimpleBlazorGrid.Website.Pages.Shared;

public class NavBarService
{
    public NavSection CurrentSection { get; set; }
    public DocumentationNavSection CurrentDocumentationSection { get; set; }

    public event EventHandler<NavSection>? OnChanged;
    public event EventHandler<DocumentationNavSection>? OnDocumentationChanged; 

    public void SetNavBarSection(NavSection navSection)
    {
        CurrentSection = navSection;
        OnChanged?.Invoke(this, CurrentSection);
    }

    public void SetDocumentationNavBarSection(DocumentationNavSection documentationNavSection)
    {
        CurrentDocumentationSection = documentationNavSection;
        OnDocumentationChanged?.Invoke(this, CurrentDocumentationSection);
    }
}

public enum NavSection
{
    Index,
    Features,
    Documentation,
}

public enum DocumentationNavSection
{
    Overview,
    GettingStarted,
    Configuration,
    Editing,
    SimpleGrid,
    SimpleGridDataSource,
    SimpleColumn,
    SimpleFilters,
    SimpleDateFilter,
    SimpleDateRangeFilter,
    SimpleEnumFilter,
    SimpleNumericFilter,
    SimpleNumericRangeFilter,
    SimpleStringFilter,
    EntityFramework,
    Searching
}