using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello1.Models.ViewModel
{
    public class ViewMovie
    {
        public Movie Movie { get; set; }

        public IEnumerable<Genre> Genres { get; set; }
    }
}