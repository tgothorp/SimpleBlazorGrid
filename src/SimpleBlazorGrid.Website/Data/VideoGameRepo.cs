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
        _videoGames.Add(new VideoGame(26, "The Legend of Zelda: Breath of the Wild", new DateOnly(2017, 04, 03), 96, Platform.WiiU, "Nintendo EPD", "Nintendo"));
        _videoGames.Add(new VideoGame(27, "BioShock", new DateOnly(2007, 08, 21), 96, Platform.Xbox360, "2K Boston, 2K Australia", "2K"));
        _videoGames.Add(new VideoGame(28, "GoldenEye 007", new DateOnly(1997, 08, 25), 96, Platform.Nintendo64, "Rare", "Nintendo"));
        _videoGames.Add(new VideoGame(29, "Uncharted 2: Among Thieves", new DateOnly(2009, 10, 13), 96, Platform.Playstation3, "Naughty Dog", "Sony Computer Entertainment"));
        _videoGames.Add(new VideoGame(30, "Baldur's Gate 3", new DateOnly(2023, 09, 06), 96, Platform.Playstation5, "Larian Studios", "Larian Studios"));
        _videoGames.Add(new VideoGame(31, "Resident Evil 4", new DateOnly(2005, 01, 11), 96, Platform.GameCube, "Capcom Production Studio 4", "Capcom"));
        _videoGames.Add(new VideoGame(32, "The Orange Box", new DateOnly(2007, 10, 10), 96, Platform.Xbox360, "Valve", "Valve"));
        _videoGames.Add(new VideoGame(33, "The Orange Box", new DateOnly(2007, 10, 10), 96, Platform.PC, "Valve", "Valve"));
        _videoGames.Add(new VideoGame(34, "Batman: Arkham City", new DateOnly(2011, 10, 18), 96, Platform.Playstation3, "Rocksteady Studios", "Warner Bros. Interactive"));
        _videoGames.Add(new VideoGame(35, "Tekken 3", new DateOnly(1998, 04, 28), 96, Platform.Playstation, "Namco", "Namco"));
        _videoGames.Add(new VideoGame(36, "Elden Ring", new DateOnly(2022, 02, 25), 96, Platform.XboxSeriesX, "FromSoftware", "Bandai Namco"));
        _videoGames.Add(new VideoGame(37, "Baldur's Gate 3", new DateOnly(2023, 08, 03), 96, Platform.PC, "Larian Studios", "Larian Studios"));
        _videoGames.Add(new VideoGame(38, "Mass Effect 2", new DateOnly(2010, 01, 26), 96, Platform.Xbox360, "BioWare", "Electronic Arts"));
        _videoGames.Add(new VideoGame(39, "The House in Fata Morgana - Dreams of the Revenants Edition", new DateOnly(2021, 04, 09), 96, Platform.Switch, "Novectacle", "Limited Run Games"));
        _videoGames.Add(new VideoGame(40, "The Legend of Zelda: Twilight Princess", new DateOnly(2006, 12, 11), 96, Platform.GameCube, "Nintendo EAD", "Nintendo"));
        _videoGames.Add(new VideoGame(41, "The Elder Scrolls V: Skyrim", new DateOnly(2011, 11, 11), 96, Platform.Xbox360, "Bethesda Game Studios", "Bethesda Softworks"));
        _videoGames.Add(new VideoGame(42, "Half-Life", new DateOnly(1998, 11, 19), 96, Platform.PC, "Valve", "Valve"));
        _videoGames.Add(new VideoGame(43, "Elden Ring", new DateOnly(2022, 02, 25), 96, Platform.Playstation5, "FromSoftware", "Bandai Namco"));
        _videoGames.Add(new VideoGame(44, "Resident Evil 4", new DateOnly(2005, 01, 11), 96, Platform.Playstation2, "Capcom Production Studio 4", "Capcom"));
        _videoGames.Add(new VideoGame(45, "The Legend of Zelda: The Wind Waker", new DateOnly(2003, 03, 23), 96, Platform.GameCube, "Nintendo EAD", "Nintendo"));
        _videoGames.Add(new VideoGame(46, "Gran Turismo", new DateOnly(1998, 04, 30), 96, Platform.Playstation, "Japan Studio", "Sony Computer Entertainment"));
        _videoGames.Add(new VideoGame(47, "The Legend of Zelda: Tears of the Kingdom", new DateOnly(2023, 05, 12), 96, Platform.Switch, "Nintendo EPD", "Nintendo"));
        _videoGames.Add(new VideoGame(48, "BioShock", new DateOnly(2007, 08, 21), 96, Platform.PC, "2K Boston, 2K Australia", "2K"));
        _videoGames.Add(new VideoGame(49, "Metal Gear Solid 2: Sons of Liberty", new DateOnly(2001, 11, 12), 96, Platform.Playstation2, "Konami Computer Entertainment Japan", "Konami"));
        _videoGames.Add(new VideoGame(49, "Grand Theft Auto Double Pack", new DateOnly(2003, 10, 31), 96, Platform.Xbox360, "Rockstar Studios", "Rockstar Games"));
    }

    public List<VideoGame> GetAllVideoGames()
    {
        return _videoGames;
    }
}