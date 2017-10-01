using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WeddingsPlanner.Models.Enums;

namespace WeddingsPlanner.Models
{
    [Table("Gifts")]
    public class Gift : Present
    {
        [Required]
        public string Name { get; set; }

        public PresentSize Size { get; set; }

    }
}
