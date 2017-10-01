using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst_Users.Models
{
    class User
    {
        public int USerId { get; set; }

        [Required]
        [MaxLength(30), MinLength(4)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(50), MinLength(6)]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Range()]
        public byte[] ProfilePicture  { get; set; }

        public DateTime RegisteredOn  { get; set; }

        public DateTime LastTimeLoggedIn  { get; set; }

        [Range(1, 120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }

    }
}
