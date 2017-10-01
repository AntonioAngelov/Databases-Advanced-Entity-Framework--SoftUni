namespace StudentSystem.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Resource
    {
        public Resource()
        {
            this.Licenses = new HashSet<License>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public ResourceType Type { get; set; }

        [Required]
        public string URL { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<License> Licenses { get; set; }

    }
}
