using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
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

        public int Prix { get; set; }
      
        public virtual ICollection<Genre> Genres { get; set; }


  

    }
    public static class BookExtensions
    {
        public static BookDTO ToBookDTO(this Book book)
        {
            return new BookDTO
            {
                Id = book.Id,
                Titre = book.Titre,
                Prix = book.Prix,
                Auteur = book.Auteur,
                Genres = book.Genres
            };
        }
    }

    public class BookDTO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }


        public String Titre { get; set; }



        public String Auteur { get; set; }

        public int Prix { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }


    }
}
