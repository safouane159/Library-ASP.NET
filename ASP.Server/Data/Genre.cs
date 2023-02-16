using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP.Server.Model
{
    public class Genre
    {

       

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public String Label { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }

}

