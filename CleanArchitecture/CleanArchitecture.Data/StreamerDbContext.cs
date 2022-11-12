using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Data
{
    public class StreamerDbContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=localdb\MSSQLLocalDB;Database=Plataforma;Trusted_Connection=True;MultipleActiveResultServer
            //Initial Catalog=Streamer;Integrated Security=True");
            //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;DESKTOP-OUBPS3E\TEW_SQLEXPRESS
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;
            Initial Catalog=Streamer;Integrated Security=True")

                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
               .EnableSensitiveDataLogging();                      
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
