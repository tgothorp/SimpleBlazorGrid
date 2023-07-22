namespace SimpleBlazorGrid.Site.Data;

public class Personnel
{
    public Personnel(long id,
        string title,
        string firstName,
        string lastName,
        DateOnly dateOfBirth,
        string jobTitle,
        string department,
        SecurityClearance securityClearance,
        string education)
    {
        Id = id;
        Title = title;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        JobTitle = jobTitle;
        Department = department;
        SecurityClearance = securityClearance;
        Education = education;
    }

    public long Id { get; set; }
    public string Title { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public DateOnly DateOfBirth { get; private set; }
    public string JobTitle { get; private set; }
    public string Department { get; private set; }
    public SecurityClearance SecurityClearance { get; private set; }
    public string Education { get; private set; }
}

public enum SecurityClearance
{
    None,
    Level1,
    Level2,
    Level3,
    Level4,
    Level5,
    Level6,
}