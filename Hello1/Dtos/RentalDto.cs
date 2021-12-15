using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hello1.Dtos
{
    public class RentalDto
    {
        public int ID { get; set; }

        [Required]
        public int CustomerID { get; set; }
        [Required]
        public int MovieID { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}