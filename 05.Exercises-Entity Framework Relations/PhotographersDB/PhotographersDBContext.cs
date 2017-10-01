namespace PhotographersDB
{
    using PhotographersDB.Migrations;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class PhotographersDBContext : DbContext
    {
        
        public PhotographersDBContext()
            : base("name=PhotographersDBContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PhotographersDBContext, Configuration>());
        }

        public virtual DbSet<Photographer> Photographers { get; set; }

        public virtual DbSet<Album> Albums { get; set; }

        public virtual DbSet<Picture> Pictures { get; set; }

        public virtual DbSet<Tag> Tags { get; set; }


    }

}