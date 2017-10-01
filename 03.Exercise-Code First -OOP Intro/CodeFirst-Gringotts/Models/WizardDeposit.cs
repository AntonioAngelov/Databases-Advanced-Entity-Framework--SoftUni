namespace CodeFirst_Gringotts.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel.DataAnnotations;


    public class WizardDeposit
    {
        public int WizardDepositId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(60)]
        public string LastName { get; set; }

        public string Notes { get; set; }

        [Required]
        [Range(1, 2147483647)]
        public int Age { get; set; }

        [StringLength(100)]
        public string MagicWandCreator { get; set; }

        [Range(1, 32767)]
        public short? MagicWandSize { get; set; }

        [StringLength(20)]
        public string DepositGroup { get; set; }

        public DateTime? DepositStartDate { get; set; }

        public double? DepositAmount { get; set; }

        public double? DepositInterest { get; set; }

        public double? DepositCharge { get; set; }

        public DateTime? DepositExpirationDate { get; set; }

        public bool? IsDepositExpired { get; set; }
    }
}
