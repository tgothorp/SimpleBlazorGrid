namespace SimpleBlazorGrid.Website.Data;

public class VideoGame
{
    public VideoGame(int rank, 
        string name, 
        DateOnly releaseDate, 
        short metascore, 
        Platform platform, 
        string developer, 
        string publisher)
    {
        Rank = rank;
        Name = name;
        ReleaseDate = releaseDate;
        Metascore = metascore;
        Platform = platform;
        Developer = developer;
        Publisher = publisher;
    }
    
    public int Rank { get; set; }
    public string Name { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public short Metascore { get; set; }
    public Platform Platform { get; set; }
    public string Developer { get; set; }
    public string Publisher { get; set; }
}