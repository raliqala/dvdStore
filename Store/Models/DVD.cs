using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models
{
    public class DVD
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal RentalPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(50)]
        public string Status { get; set; }
    }
}
