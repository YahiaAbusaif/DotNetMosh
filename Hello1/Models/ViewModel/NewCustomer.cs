using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello1.Models.ViewModel
{
    public class NewCustomer
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer customer { get; set; }
    }
}