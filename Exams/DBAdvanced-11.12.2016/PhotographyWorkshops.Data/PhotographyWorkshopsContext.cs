namespace PhotographyWorkshops.Data
{
    using PhotographyWorkshops.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PhotographyWorkshopsContext : DbContext
    {
          public PhotographyWorkshopsContext()
            : base("name=PhotographyWorkshopsContext")
        {
        }

        public virtual DbSet<Lens> Lenses { get; set; }

        public virtual DbSet<Camera> Cameras { get; set; }

        public virtual DbSet<Accessory> Accessories { get; set; }

        public virtual DbSet<Photographer> Photographers { get; set; }

        public virtual DbSet<Workshop> Workshops { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lens>()
                .Property(l => l.MaxAperture)
                .HasPrecision(18, 1);

           
            modelBuilder.Entity<Camera>()
                .Map<DSLRCamera>(m => m.Requires("Discriminator").HasValue("DSLRCamera"))
                .Map<MirrorlessCamera>(m => m.Requires("Discriminator").HasValue("MirrorlessCamera"));

            modelBuilder.Entity<Workshop>()
                .HasRequired(w => w.Trainer)
                .WithMany(p => p.TrainedWorkshops)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Workshop>()
                .HasMany(w => w.Participants)
                .WithMany(p => p.Workshops);

            modelBuilder.Entity<Photographer>()
                .HasRequired(p => p.PrimaryCamera)
                .WithMany(c => c.PrimaryCameras)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photographer>()
                .HasRequired(p => p.SecondaryCamera)
                .WithMany(c => c.SecondaryCameras)
                .WillCascadeOnDelete(false);
        }
    }
    
}