using System.Collections.Generic;
using System.Text;

namespace Projection.Models
{
    using System;

    public class Employee
    {
        public Employee()
        {
            this.Subordinates = new HashSet<Employee>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public DateTime? BirthDay { get; set; }

        public string Address { get; set; }

        public Employee Manager { get; set; }

        public virtual ICollection<Employee> Subordinates { get; set; }

        }
}
