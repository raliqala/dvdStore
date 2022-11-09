using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeLibrary.Models
{
    public class UpdateRental
    {
        public Int64 Id { get; set; }
        public string dateReturned { get; set; }
        public string status { get; set; }
        public decimal overDueAmount { get; set; }
    }
}
