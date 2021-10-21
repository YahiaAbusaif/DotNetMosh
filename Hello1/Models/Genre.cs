using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hello1.Models
{
    public class Genre
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="please enter valid Genres's name")]
        [MaxLength(256)]
        public string Name { get; set; }

        public int? MoviesNumber { get; set; }

        //public virtual ICollection<Movie> Movies { get; set; }

    }
}