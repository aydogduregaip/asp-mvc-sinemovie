using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using sinemovie.Models.EntityFramework;

namespace sinemovie.Models.IEnumerable
{
    public class MoviesComments
    {
        public IEnumerable<movies> Movies { get; set; }
        public IEnumerable<comments> Comments { get; set; }

        public IEnumerable<actors> Actors { get; set; }
    }
}