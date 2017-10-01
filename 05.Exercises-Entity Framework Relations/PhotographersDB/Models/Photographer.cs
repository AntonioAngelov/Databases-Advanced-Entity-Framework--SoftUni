using System.Collections.Generic;

namespace PhotographersDB.Models
{
    using System.ComponentModel.DataAnnotations;

    using System;

    public class Photographer
    {
        public Photographer()
        {
            this.Albums = new HashSet<Album>();
        }

        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime RegisterDate { get; set; }

        public DateTime BirthDate { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

    }
}
