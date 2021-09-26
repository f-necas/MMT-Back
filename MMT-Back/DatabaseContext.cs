using Microsoft.EntityFrameworkCore;
using MMT_Back.EntityModels;

namespace MMT_Back;
public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    { }

    public DbSet<Place> Place { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        var wattabloc = new Place
        {
            Id = 1,
            Name = "Wattabloc",
            Address = "340 Chem. des Carrières, 73230 Saint-Alban-Leysse",
            Coordinate = "45.57531875608867, 5.958606899998407"
        };

        var archimalt = new Place
        {
            Id = 2,
            Name = "ARCHIMALT",
            Address = "95 Rue de Bolliet, 73230 Saint-Alban-Leysse",
            Coordinate = "45.57510386329614, 5.949666827895427"
        };

        modelBuilder.Entity<Place>().HasData(wattabloc, archimalt);

    }
}
