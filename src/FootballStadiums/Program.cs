using FootballStadiums.API.Database.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FootballStadiumDb>(opt => opt.UseInMemoryDatabase("FootballStadiums"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();