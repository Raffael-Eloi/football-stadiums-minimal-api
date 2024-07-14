using FootballStadiums.API.Database.Models;
using FootballStadiums.API.DTOs;
using FootballStadiums.API.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FootballStadiumDb>(opt => opt.UseInMemoryDatabase("FootballStadiums"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger(options =>
{
    options.RouteTemplate = "openapi/{documentName}/football-stadium-schema.json";
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("v1/football-stadium-schema.json", "Football Stadiums API");
    options.RoutePrefix = $"openapi";
});

app.MapPost("/seed", async (FootballStadiumDb db) =>
{
    IEnumerable<FootballStadium> stadiums =
    [
        new FootballStadium { Name = "Old Trafford", Country = "England" },
        new FootballStadium { Name = "Santiago Bernabeu", Country = "Spain" },
        new FootballStadium { Name = "Allianz Arena", Country = "Germany" },
        new FootballStadium { Name = "San Siro", Country = "Italy" },
        new FootballStadium { Name = "Parc des Princes", Country = "France" }
    ];

    await db.FootballStadiums.AddRangeAsync(stadiums);
    await db.SaveChangesAsync();

    return Results.Created("/seed", "Seeded database with 5 stadiums");
});

app.MapPost("/stadiums", async (FootballStadiumDb db, AddFootballStadiumRequest request) => {
    FootballStadium stadium = new()
    {
        Name = request.Name,
        Country = request.Country
    };

    await db.FootballStadiums.AddAsync(stadium);
    await db.SaveChangesAsync();

    return Results.Created($"/stadiums/{stadium.Id}", stadium);
}).Produces<FootballStadium>();


app.MapGet("/stadiums", async (FootballStadiumDb db) =>
{
    return Results.Ok(await db.FootballStadiums.ToListAsync());
}).Produces<List<FootballStadium>>();


app.MapGet("/stadiums/{id}", async (FootballStadiumDb db, Guid id) =>
{
    FootballStadium? stadium = await db.FootballStadiums.FindAsync(id);

    if (stadium is null)
    {
        return Results.NotFound();
    }

    return Results.Ok(stadium);
}).Produces<FootballStadium>();


app.MapPut("/stadiums/{id}", async (FootballStadiumDb db, Guid id, AddFootballStadiumRequest request) =>
{
    FootballStadium? stadium = await db.FootballStadiums.FindAsync(id);

    if (stadium is null)
    {
        return Results.NotFound();
    }

    stadium.Name = request.Name;
    stadium.Country = request.Country;

    db.FootballStadiums.Update(stadium);
    await db.SaveChangesAsync();

    return Results.NoContent();
});


app.MapDelete("/stadiums/{id}", async (FootballStadiumDb db, Guid id) =>
{
    FootballStadium? stadium = await db.FootballStadiums.FindAsync(id);

    if (stadium is null)
    {
        return Results.NotFound();
    }

    db.FootballStadiums.Remove(stadium);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();