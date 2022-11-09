using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models
{
    public class CustomerRental
    {
        [StringLength(50)]
        public string status { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal overDueAmount { get; set; }
    }
}
