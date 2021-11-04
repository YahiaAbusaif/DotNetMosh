using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello1.Dtos
{
    public class GenreDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public IList<MovieDto>  Movies { get; set; }
    }
}