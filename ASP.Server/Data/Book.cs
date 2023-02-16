﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YamlDotNet.Core.Tokens;


namespace ASP.Server.Model
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }


        public String Titre { get; set; }

        public String Contenu { get; set; }

        public String Auteur { get; set; }

        public Double Prix { get; set; }
      
        public virtual ICollection<Genre> Genres { get; set; }


    }
}
