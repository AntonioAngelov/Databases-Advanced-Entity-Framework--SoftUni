namespace PlanetHunters.Data
{
    using PlanetHunters.Models;

    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PlanetHuntersContext : DbContext
    {
         public PlanetHuntersContext()
            : base("name=PlanetHuntersContext")
        {
        }
        
        public virtual DbSet<Astronomer> Astronomers { get; set; }

        public virtual DbSet<Discovery> Discoveries { get; set; }

        public virtual DbSet<Telescope> Telescopes { get; set; }

        public virtual DbSet<StarSystem> StarSystems { get; set; }

        public virtual DbSet<Star> Stars { get; set; }

        public virtual DbSet<Planet> Planets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Astronomer>()
                .HasMany(a => a.ObservedDiscoveries)
                .WithMany(d => d.Observers)
                .Map(cfg =>
                {
                    cfg.MapLeftKey("ObserverId");
                    cfg.MapRightKey("DiscoveryId");
                    cfg.ToTable("ObserversDiscoveries");
                });


            modelBuilder.Entity<Astronomer>()
                .HasMany(a => a.PioneeringDiscoveries)
                .WithMany(d => d.Pioneers)
                .Map(cfg =>
                {
                    cfg.MapLeftKey("PioneerId");
                    cfg.MapRightKey("DiscoveryId");
                    cfg.ToTable("PioneerDiscoveries");
                });

        }
    }

}