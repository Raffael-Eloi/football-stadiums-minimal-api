using FootballStadiums.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballStadiums.API.Database.Models;

public class FootballStadiumDb(DbContextOptions<FootballStadiumDb> options) : DbContext(options)
{
    public DbSet<FootballStadium> FootballStadiums  => Set<FootballStadium>();
}