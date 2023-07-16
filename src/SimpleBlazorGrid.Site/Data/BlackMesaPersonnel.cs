namespace SimpleBlazorGrid.Site.Data;

public static class BlackMesaPersonnel
{
    private static List<Personnel> _blackMesaPersonnel = new();

    public static List<Personnel> GetPersonnel()
    {
        if (_blackMesaPersonnel is { Count: > 0 })
            return _blackMesaPersonnel;

        _blackMesaPersonnel = new List<Personnel>
        {
            new(5352,
                "Dr",
                "Gordon",
                "Freeman",
                new DateOnly(1978, 11, 19),
                "Theoretical Physicist",
                "Sector C - Test Labs",
                SecurityClearance.Level3,
                "Ph.D MIT"),

            new(8957,
                "Mr",
                "Barney",
                "Calhoun",
                new DateOnly(),
                "Security Officer",
                "Black Mesa Security",
                SecurityClearance.Level3,
                "Martinson College"),

            new(2534,
                "Prof",
                "Isaac",
                "Kleiner",
                new DateOnly(),
                "Lead Theoretical Physicist",
                "Sector C - Test Labs",
                SecurityClearance.Level4,
                "Ph.D, Prof, MIT"),

            new(4488,
                "Dr",
                "Colette",
                "Green",
                new DateOnly(),
                "Level 4 Research Associate",
                "Sector C - Anomalous Materials",
                SecurityClearance.Level4,
                "Ph.D Carnegie Mellon"),

            new(4487,
                "Dr",
                "Gina",
                "Cross",
                new DateOnly(),
                "Hazardous Environment Supervisor",
                "Sector C - Anomalous Materials",
                SecurityClearance.Level4,
                "Ph.D Caltech"),

            new(2439,
                "Dr",
                "Eli",
                "Vance",
                new DateOnly(),
                "Lead Theoretical Physicist",
                "Sector C - Test Labs",
                SecurityClearance.Level4,
                "Ph.D Harvard"),

            new(9901,
                "Dr",
                "Wallace",
                "Breen",
                new DateOnly(),
                "CLASSIFIED",
                "CLASSIFIED",
                SecurityClearance.Level6,
                "CLASSIFIED")
        };

        return _blackMesaPersonnel;
    }
}