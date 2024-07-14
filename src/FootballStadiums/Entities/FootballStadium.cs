namespace FootballStadiums.API.Entities;

public class FootballStadium
{
    public FootballStadium()
    {
        Name = string.Empty;
        Country = string.Empty;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Country { get; set; }
}