using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hello1.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }


        public string Dirctor { get; set; }
        public DateTime? Release { get; set; }



    }
}