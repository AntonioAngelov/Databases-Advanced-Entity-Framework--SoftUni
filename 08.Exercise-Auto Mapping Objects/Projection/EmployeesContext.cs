namespace Projection
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Projection.Models;

    public class EmployeesContext : DbContext
    {
        
        public EmployeesContext()
            : base("name=EmployeesContext")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
    }

}