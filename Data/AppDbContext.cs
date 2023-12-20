
namespace GamesMvc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<GameDevices> GameDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Sports" },
                new Category { Id = 2, Name = "ِAction" },
                new Category { Id = 3, Name = "Adventure" },
                new Category { Id = 4, Name = "Racing" },
                new Category { Id = 5, Name = "Fighting" },
                new Category { Id = 6, Name = "Film" }
                );

            modelBuilder.Entity<Device>().HasData(
                new Device { Id = 1, Name = "Play Station", Icon = "bi bi-playstation" },
                new Device { Id = 2, Name = "X Box", Icon = "bi bi-xbox" },
                new Device { Id = 3, Name = "Nitendo Switch", Icon = "bi bi-nitendo-switch" },
                new Device { Id = 4, Name = "PC", Icon = "bi bi-pc-display" }
                );

            modelBuilder.Entity<GameDevices>().HasKey(x => new { x.DeviceId, x.GameId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
