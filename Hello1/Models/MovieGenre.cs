using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello1.Models
{
    public class MovieGenre
    {
        public int ID { get; set; }

        public int GenreID { get; set; }

        public Genre Genre { get; set; }


        public int MovieID { get; set; }

        public Movie Movie { get; set; }

    }
}