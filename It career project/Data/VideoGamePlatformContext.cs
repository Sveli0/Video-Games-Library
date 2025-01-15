using It_career_project.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace It_career_project.Data
{
    public class VideoGamePlatformContext : DbContext
    {
        public VideoGamePlatformContext()
        {

        }

        public VideoGamePlatformContext(DbContextOptions options)
            : base(options)
        {

        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<GameStudio> GameStudios { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VideoGame> VideoGames { get; set; }
        public virtual DbSet<UserGameCollection> UsersGameCollections { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<GiftCard> GiftCards { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<VideoGame>()
                .Property(x => x.Price)
                .HasPrecision(5, 2);
            modelBuilder
                .Entity<Country>()
                .Property(x => x.CurrencyExchangeRateToEuro)
                .HasPrecision(7, 5);
            modelBuilder
                .Entity<Sale>()
                .Property(x => x.Discount)
                .HasPrecision(3);
            modelBuilder
                .Entity<Sale>()
                .Property(p => p.StartDate)
                .HasColumnType("date");
            modelBuilder
                .Entity<Sale>()
                .Property(p => p.EndDate)
                .HasColumnType("date");
            modelBuilder
                .Entity<UserGameCollection>()
                .HasKey(x => new { x.GameId, x.UserId });
            modelBuilder
                .Entity<Review>()
                .Property(p => p.ReviewText)
                .HasColumnType("text");
            modelBuilder
                .Entity<Review>()
                .HasKey(x => new { x.GameId, x.UserId });
            modelBuilder
                .Entity<GiftCard>()
                .Property(x => x.Value)
                .HasPrecision(5, 2);
            modelBuilder
                .Entity<User>()
                .HasIndex(x => x.Username)
                .IsUnique();
            modelBuilder
                .Entity<Admin>()
                .HasIndex(x => x.Username)
                .IsUnique();
            modelBuilder
                .Entity<GameStudio>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder
                .Entity<Country>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder
                .Entity<VideoGame>()
                .HasIndex(x => x.GameTitle)
                .IsUnique();
            modelBuilder
                .Entity<GiftCard>()
                .HasIndex(x => x.Code)
                .IsUnique();
            modelBuilder
                .Entity<Genre>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<VideoGame>()
                .Property(x => x.IsAvailable)
                .HasDefaultValue(true);
            modelBuilder.Entity<GameStudio>()
                .Property(x => x.UnderContract)
                .HasDefaultValue(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}