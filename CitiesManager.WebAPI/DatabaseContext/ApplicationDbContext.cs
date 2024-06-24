using CitiesManager.WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace CitiesManager.WebAPI.DatabaseContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions option) : base(option)
    {
    }

    public ApplicationDbContext()
    {
    }
    public virtual DbSet<City> Citys { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<City>().HasData(new City() { CityId = Guid.Parse("4416D252-1F58-42E9-89D5-1623CFBDCD87"), CityName = "London" });
        modelBuilder.Entity<City>().HasData(new City() { CityId = Guid.Parse("408685DF-6769-43E1-9031-BFADA660B69D"), CityName = "New York" });
    }
}