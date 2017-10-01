namespace PhotographersDB.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Picture
    {
        public Picture()
        {
            this.Albums = new HashSet<Album>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Caption { get; set; }

        [Required]
        public string PathToPicture { get; set; }

        public virtual ICollection<Album> Albums { get; set; }
    }
}
