using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographyWorkshops.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Photographer
    {
        public Photographer()
        {
            this.Lenses = new HashSet<Lens>();
            this.Accessories = new HashSet<Accessory>();
            this.Workshops = new HashSet<Workshop>();
            this.TrainedWorkshops = new HashSet<Workshop>();
        }

        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [RegularExpression(@"^\+[0-9]{1,3}\/[0-9]{8,10}$")]
        public string Phone { get; set; }

        [ForeignKey("PrimaryCamera")]
        public virtual int PrimaryCameraId { get; set; }

        [Required]
        public virtual Camera PrimaryCamera { get; set; }

        [ForeignKey("SecondaryCamera")]
        public virtual int SecondaryCameraId { get; set; }

        [Required]
        public virtual Camera SecondaryCamera { get; set; }

        public virtual ICollection<Lens> Lenses { get; set; }

        public virtual ICollection<Accessory> Accessories { get; set; }

        public virtual ICollection<Workshop> Workshops { get; set; }

        public virtual ICollection<Workshop> TrainedWorkshops { get; set; }
    }
}
