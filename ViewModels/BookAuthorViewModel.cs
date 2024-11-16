using BooksStore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BooksStore.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookID { get; set; }
        [Required]
        [StringLength(20,MinimumLength =5)]
        public string Title { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public List<Author> Authors { get; set; }
        public IFormFile File { get; set; } /* this is file that we will work with it  */
        public string Image { get; set; }
          
        public string notes{ get; set; }


    }
}
