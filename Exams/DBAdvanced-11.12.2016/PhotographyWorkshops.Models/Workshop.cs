using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotographyWorkshops.Models
{
    public class Workshop
    {
        public Workshop()
        {
            this.Participants = new HashSet<Photographer>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public decimal? PricePerParticipant { get; set; }

        [Required]
        public virtual Photographer Trainer { get; set; }

        public virtual ICollection<Photographer> Participants { get; set; }
    }
}
