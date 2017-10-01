using WeddingsPlanner.Data.Migrations;
using WeddingsPlanner.Models.Enums;

namespace WeddingsPlanner.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class WeddingsPlannerContext : DbContext
    {
        // Your context has been configured to use a 'WeddingsPlannerContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WeddingsPlanner.Data.WeddingsPlannerContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'WeddingsPlannerContext' 
        // connection string in the application configuration file.
        public WeddingsPlannerContext()
            : base("name=WeddingsPlannerContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WeddingsPlannerContext, Configuration>());
        }

        public virtual DbSet<Agency> Agencies { get; set; }

        public virtual DbSet<Present> Presents { get; set; }

        public virtual DbSet<Invitation> Invitations { get; set; }

        public virtual DbSet<Person> People { get; set; }

        public virtual DbSet<Venue> Venues { get; set; }

        public virtual DbSet<Wedding> Weddings { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Present>()
                .HasKey(p => p.InvitationId);

            modelBuilder.Entity<Invitation>()
                .HasRequired(i => i.Present)
                .WithRequiredPrincipal(p => p.Invitation);

            modelBuilder.Entity<Wedding>()
                .HasRequired(w => w.Bride)
                .WithMany(p => p.Brides)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Wedding>()
               .HasRequired(w => w.Bridegroom)
               .WithMany(p => p.Bridegrooms)
               .WillCascadeOnDelete(false);
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}