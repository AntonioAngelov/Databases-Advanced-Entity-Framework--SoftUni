using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingsPlanner.Models
{
    using WeddingsPlanner.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Person
    {
        public Person()
        {
            this.Bridegrooms = new HashSet<Wedding>();
            this.Brides = new HashSet<Wedding>();
            this.Invitations = new HashSet<Invitation>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 1)]
        public string MiddleNameInitial { get; set; }

        [Required]
        [MinLength(2)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{this.FirstName} {this.MiddleNameInitial} {this.LastName}";
            }
        }

        public Gender Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        [NotMapped]
        public int? Age
        {
            get
            {
                if (this.BirthDate == null) return null;
                else
                {
                    DateTime now = DateTime.Now;
                    int age = now.Year - ((DateTime)this.BirthDate).Year;

                    if (now.Month < ((DateTime)this.BirthDate).Month ||
                        now.Month < ((DateTime)this.BirthDate).Month &&
                        now.Day < ((DateTime)this.BirthDate).Day)
                    {
                        age--;
                    }
                    return age;

                }
            }
        }

        public string Phone { get; set; }

        [RegularExpression(@"[a-zA-Z0-9]+@[a-z]{1,}.[a-z]{1,}")]
        public string Email { get; set; }

        public virtual ICollection<Invitation> Invitations { get; set; }
        [InverseProperty("Bride")]

        public virtual ICollection<Wedding> Brides { get; set; }

        [InverseProperty("Bridegroom")]
        public virtual ICollection<Wedding> Bridegrooms { get; set; }
    }
}
