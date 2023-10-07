namespace SimpleBlazorGrid.Website.Data;

public class Movie
{
    public Movie(int rank, 
        string name, 
        string director, 
        DateOnly released, 
        int runtime, 
        decimal stars, 
        decimal gross, 
        string genre)
    {
        Rank = rank;
        Name = name;
        Director = director;
        Released = released;
        Runtime = runtime;
        Stars = stars;
        Gross = gross;
        Genre = genre;
    }
    
    public int Rank { get; set; }
    public string Name { get; set; }
    public string Director { get; set; }
    public DateOnly Released { get; set; }
    public int Runtime { get; set; }
    public decimal Stars { get; set; }
    public decimal Gross { get; set; }
    public string Genre { get; set; }
}