using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using YamlDotNet.Core.Tokens;


namespace ASP.Server.Model
{
    public class Auteur
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }


        public String Name { get; set; }
  

        public Double Age { get; set; }

        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }




    }
    
}
