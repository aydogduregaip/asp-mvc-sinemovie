//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sinemovie.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class actors
    {
        public int id { get; set; }
        public string name { get; set; }
        public string poster { get; set; }
        public int movie_id { get; set; }
        public string biography { get; set; }
        public string gender { get; set; }
        public Nullable<int> age { get; set; }
        public string country { get; set; }
    
        public virtual movies movies { get; set; }
    }
}
