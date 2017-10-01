using MassDefect.Models;

namespace MassDefect.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MassDefectContext : DbContext
    {
        public MassDefectContext()
            : base("name=MassDefectContext")
        {
        }

        public virtual DbSet<SolarSystem> SolarSystems { get; set; }

        public virtual DbSet<Star> Stars { get; set; }

        public virtual DbSet<Planet> Planets { get; set; }

        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<Anomaly> Anomalies { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anomaly>()
                .HasRequired(a => a.OriginPlanet)
                .WithMany(p => p.OriginAnomalies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
                .HasRequired(a => a.TeleportPlanet)
                .WithMany(p => p.TargettingAnomalies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Anomalies)
                .WithMany(a => a.Victims)
                .Map(av =>
                {
                    av.MapLeftKey("AnomalyId");
                    av.MapRightKey("PersonId");
                    av.ToTable("AnomalyVictims");
                });

            modelBuilder.Entity<Star>()
                .HasRequired(s => s.SolarSystem)
                .WithMany(s => s.Stars)
                .WillCascadeOnDelete(false);
        }
    }


}