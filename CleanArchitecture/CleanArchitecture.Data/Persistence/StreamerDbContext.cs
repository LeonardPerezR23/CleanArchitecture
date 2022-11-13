using CleanArchitecture.Domain;
using CleanArchitecture.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContext : DbContext
    {
        public StreamerDbContext(DbContextOptions<StreamerDbContext> options) : base(options)
        {
        }







        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //optionsBuilder.UseSqlServer(@"Data Source=localdb\MSSQLLocalDB;Database=Plataforma;Trusted_Connection=True;MultipleActiveResultServer
        //Initial Catalog=Streamer;Integrated Security=True");
        //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;DESKTOP-OUBPS3E\TEW_SQLEXPRESS
        //     optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;
        //     Initial Catalog=Streamer;Integrated Security=True")

        //         .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
        //        .EnableSensitiveDataLogging();
        //  }



        public override Task<int> SaveChangesAsync(CancellationToken cancellation = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseDomainModel>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "system";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedBy = "sistem";
                        break;

                }
            }

            return base.SaveChangesAsync(cancellation);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)  //ESTO ES CUANDO LA FORANEA NO ESTA DEFINIDA
        {
            modelBuilder.Entity<Streamer>()
                .HasMany(m => m.Videos)
                .WithOne(m => m.Streamer)
                .HasForeignKey(m => m.StreamerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Video>()
                .HasMany(p => p.Actores)
                .WithMany(t => t.Videos)
                .UsingEntity<VideoActor>(
                pt => pt.HasKey(e => new { e.ActorId, e.VideoId })
                );
        }

        public DbSet<Streamer>? Streamers { get; set; }

        public DbSet<Video>? Videos { get; set; }

        public DbSet<Actor>? Actores { get; set; }

    }
}
