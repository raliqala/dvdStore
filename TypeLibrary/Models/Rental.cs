using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class Rental
    {
        public Int64 dvdId { get; set; }
        public Int64 customerId { get; set; }
        public string rentalDate { get; set; }
        public string returnDate { get; set; }
        public string status { get; set; }
        public decimal overDueAmount { get; set; }
    }
}
