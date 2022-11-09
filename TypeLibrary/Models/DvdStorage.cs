using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class DvdStorage
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal RentalPrice { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public Int64 adminId { get; set; }
        public string Slug { get; set; }
    }
}
