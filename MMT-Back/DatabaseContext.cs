
using Microsoft.EntityFrameworkCore;
using MMT_Back.EntityModels;
using NetTopologySuite.Geometries;

namespace MMT_Back;
public class DatabaseContext : DbContext
{

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    { }

    public DbSet<User> Users { get; set; }
    public DbSet<Friend> Friend { get; set; }
    public DbSet<Place> Place { get; set; }
    public DbSet<Invitation> Invitation { get; set; }
    public DbSet<UserEvent> UserEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       

        modelBuilder.Entity<Friend>()
                    .HasOne(a => a.RequestedBy)
                    .WithMany(b => b.SentFriendRequests)
                    .HasForeignKey(c => c.RequestedById).IsRequired().OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Friend>()
            .HasOne(a => a.RequestedTo)
            .WithMany(b => b.ReceievedFriendRequests)
            .HasForeignKey(c => c.RequestedToId).IsRequired().OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Invitation>().HasOne(c => c.InvitedUser)
                .WithMany().OnDelete(DeleteBehavior.ClientNoAction);

        var wattabloc = new Place
        {
            Id = 1,
            Name = "Wattabloc",
            Address = "340 Chem. des Carrières, 73230 Saint-Alban-Leysse"
        };

        var archimalt = new Place
        {
            Id = 2,
            Name = "Archimalt",
            Address = "95 Rue de Bolliet, 73230 Saint-Alban-Leysse"
        };

        modelBuilder.Entity<User>()
            .Property(f => f.Id)
            .ValueGeneratedOnAdd();

        var flo = new User { Id = 1, UserName = "Flo", Password = "flo"};
        var angie = new User { Id = 2, UserName = "Angie", Password = "angie" };

        modelBuilder.Entity<Place>().HasData(wattabloc, archimalt);
        modelBuilder.Entity<User>().HasData(flo, angie);
    }
}
