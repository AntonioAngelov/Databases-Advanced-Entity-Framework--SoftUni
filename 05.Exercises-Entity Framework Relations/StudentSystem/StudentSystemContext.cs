namespace StudentSystem
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;
    using StudentSystem.Migrations;

    public class StudentSystemContext : DbContext
    {
        public StudentSystemContext()
            : base("name=StudentSystemContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentSystemContext, Configuration>());
        }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Homework> Homeworks { get; set; }

        public virtual DbSet<Resource> Resources { get; set; }

        public virtual DbSet<License> Licenses { get; set; }
    }
}