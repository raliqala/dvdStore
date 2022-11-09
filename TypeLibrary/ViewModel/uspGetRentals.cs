using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModel
{
    public class UspGetRentals
    {
        public Int64 Id { get; set; }
        public string status { get; set; }
        public decimal? overDueAmount { get; set; }
        public string rentalDate { get; set; }
        public string returnDate { get; set; }
        public string Title { get; set; }
        public decimal RentalPrice { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public long PhoneNumber { get; set; }
    }
}
