namespace SimpleBlazorGrid.Website.Data;

public class VideoGameRepo
{
    private List<VideoGame> _videoGames;

    public VideoGameRepo()
    {
        _videoGames = new List<VideoGame>();
        
        _videoGames.Add(new VideoGame(1, "The Legend of Zelda: Ocarina of Time", new DateOnly(1998, 11, 23), 99, Platform.Nintendo64, "Nintendo", "Nintendo"));
        _videoGames.Add(new VideoGame(2, "Tony Hawk's Pro Skater 2", new DateOnly(2000, 09, 20), 98, Platform.Playstation, "Neversoft", "Activision"));
        _videoGames.Add(new VideoGame(3, "Grand Theft Auto IV", new DateOnly(2008, 04, 29), 98, Platform.Playstation3, "Rockstar North", "Rockstar Games"));
        _videoGames.Add(new VideoGame(4, "SoulCalibur", new DateOnly(1999, 09, 8), 98, Platform.Dreamcast, "Project Soul", "Namco"));
        _videoGames.Add(new VideoGame(5, "Grand Theft Auto IV", new DateOnly(2008, 04, 29), 98, Platform.Xbox360, "Rockstar North", "Rockstar Games"));
        _videoGames.Add(new VideoGame(6, "Super Mario Galaxy", new DateOnly(2007, 11, 12), 97, Platform.Wii, "Nintendo", "Nintendo"));
        _videoGames.Add(new VideoGame(7, "Super Mario Galaxy 2", new DateOnly(2010, 05, 23), 97, Platform.Wii, "Nintendo", "Nintendo"));
        _videoGames.Add(new VideoGame(8, "Red Dead Redemption 2", new DateOnly(2018, 10, 26), 97, Platform.XboxOne, "Rockstar Studios", "Rockstar Games"));
        _videoGames.Add(new VideoGame(9, "Grand Theft Auto V", new DateOnly(2014, 11, 18), 97, Platform.XboxOne, "Rockstar Studios", "Rockstar Games"));
        _videoGames.Add(new VideoGame(10, "Grand Theft Auto V", new DateOnly(2013, 09, 17), 97, Platform.Playstation3, "Rockstar Studios", "Rockstar Games"));
        _videoGames.Add(new VideoGame(11, "Disco Elysium: The Final Cut", new DateOnly(2021, 04, 30), 97, Platform.PC, "ZA/UM", "ZA/UM"));
        _videoGames.Add(new VideoGame(12, "Grand Theft Auto V", new DateOnly(2013, 09, 17), 97, Platform.Xbox360, "Rockstar Studios", "Rockstar Games"));
        _videoGames.Add(new VideoGame(13, "Tony Hawk's Pro Skater 2", new DateOnly(2000, 09, 06), 97, Platform.Dreamcast, "Neversoft", "Activision"));
        _videoGames.Add(new VideoGame(14, "The Legend of Zelda: Breath of the Wild", new DateOnly(2017, 04, 03), 97, Platform.Switch, "Nintendo EPD", "Nintendo"));
        _videoGames.Add(new VideoGame(15, "Tony Hawk's Pro Skater 3", new DateOnly(2001, 10, 28), 97, Platform.Playstation2, "Neversoft", "Activision"));
        _videoGames.Add(new VideoGame(16, "Perfect Dark", new DateOnly(2000, 04, 22), 97, Platform.Nintendo64, "Rare", "Rare"));
        _videoGames.Add(new VideoGame(17, "Red Dead Redemption 2", new DateOnly(2018, 11, 28), 97, Platform.Playstation4, "Rockstar Studios", "Rockstar Games"));
        _videoGames.Add(new VideoGame(18, "Grand Theft Auto V", new DateOnly(2014, 11, 18), 97, Platform.Playstation4, "Rockstar Studios", "Rockstar Games"));
        _videoGames.Add(new VideoGame(19, "Metroid Prime", new DateOnly(2002, 11, 17), 97, Platform.GameCube, "Retro Studios", "Rockstar Games"));
        _videoGames.Add(new VideoGame(20, "Grand Theft Auto III", new DateOnly(2001, 10, 22), 97, Platform.Playstation2, "DMA Design", "Rockstar Games"));
        _videoGames.Add(new VideoGame(21, "Super Mario Odyssey", new DateOnly(2017, 10, 27), 97, Platform.Switch, "Nintendo EPD", "Nintendo"));
        _videoGames.Add(new VideoGame(22, "Halo: Combat Evolved", new DateOnly(2001, 11, 14), 97, Platform.Xbox, "Bungie", "Microsoft Game Studios"));
        _videoGames.Add(new VideoGame(23, "NFL 2K1", new DateOnly(2000, 09, 7), 97, Platform.Dreamcast, "Visual Concepts", "Sega"));
        _videoGames.Add(new VideoGame(24, "Half-Life 2", new DateOnly(2004, 11, 16), 96, Platform.PC, "Valve", "Valve"));
        _videoGames.Add(new VideoGame(25, "Grand Theft Auto V", new DateOnly(2015, 04, 13), 96, Platform.PC, "Rockstar Studios", "Rockstar Games"));
    }

    public List<VideoGame> GetAllVideoGames()
    {
        return _videoGames;
    }
}