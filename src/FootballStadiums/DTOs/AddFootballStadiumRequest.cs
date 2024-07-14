namespace FootballStadiums.API.DTOs;

public record AddFootballStadiumRequest
{
    public AddFootballStadiumRequest()
    {
        Name = string.Empty;
        Country = string.Empty;
    }

    public string Name { get; set; }

    public string Country { get; set; }
}