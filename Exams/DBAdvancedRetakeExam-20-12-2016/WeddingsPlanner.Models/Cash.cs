using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingsPlanner.Models
{
    [Table("Cash")]
    public class Cash : Present
    {
        [Required]
        public decimal? CashAmount { get; set; }
    }
}
