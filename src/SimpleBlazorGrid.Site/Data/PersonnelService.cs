namespace SimpleBlazorGrid.Site.Data;

public class PersonnelService
{
    public List<Personnel> GetPersonnelForCompany(Company company)
    {
        return company switch
        {
            Company.BlackMesa => BlackMesaPersonnel.GetPersonnel(),
            _ => throw new ArgumentOutOfRangeException(nameof(company), company, null)
        };
    }
}

public enum Company
{
    BlackMesa,
}