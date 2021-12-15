using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello1.Models.ViewModel
{
    public class CustomerRentalList
    {
        public Customer Customer { get; set; }
        public List<Rental> RentalList { get; set; }
    }
}