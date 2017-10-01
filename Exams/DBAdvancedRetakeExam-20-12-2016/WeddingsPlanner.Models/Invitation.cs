using System.ComponentModel.DataAnnotations;
using WeddingsPlanner.Models.Enums;

namespace WeddingsPlanner.Models
{
    public class Invitation
    {
        public int Id { get; set; }

        [Required]
        public Wedding Wedding { get; set; }

        [Required]
        public virtual Person Guest { get; set; }

        public PresentSize PresentSize { get; set; }

        public bool IsAttending { get; set; }

        [Required]
        public Family Family { get; set; }

        public virtual Present Present { get; set; }

    }
}
