using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.ViewModel
{
    public class UspGetDvdList
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal RentalPrice { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public long adminId { get; set; }
        public string Slug { get; set; }
    }
}
