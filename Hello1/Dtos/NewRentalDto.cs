using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello1.Dtos
{
    public class NewRentalDto
    {
        public int CustomerID { get; set; }
        public List<int> MoviesID { get; set; }
    }
}