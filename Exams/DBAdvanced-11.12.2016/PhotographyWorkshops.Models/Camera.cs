using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotographyWorkshops.Models
{
    public class Camera
    {
        public Camera()
        {
            this.PrimaryCameras = new HashSet<Photographer>();
            this.SecondaryCameras = new HashSet<Photographer>();
        }

        public int Id { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        public bool? IsFullFrame { get; set; }

        [Range(100, Int32.MaxValue)]
        public int MinISO { get; set; }

        [Range(100, Int32.MaxValue)]
        public int? MaxISO { get; set; }

        public virtual ICollection<Photographer> PrimaryCameras { get; set; }

        public virtual ICollection<Photographer> SecondaryCameras { get; set; }
    }
}
