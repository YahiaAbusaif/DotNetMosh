using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hello1.Dtos
{
    public class MovieDto
    {

         public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }


        public string Dirctor { get; set; }
        public DateTime? Release { get; set; }

        [Range(0,10)]
        [Display(Name ="Average Review out of ten")]
        public double? AverageReviewOutOfTen { get; set; }

        [Display(Name ="Number of Movie in stock")]
        public int? NumberInStock { get; set; }

        [Range(0,1000)]
        public Double? Price { get; set; }


    
    }
}