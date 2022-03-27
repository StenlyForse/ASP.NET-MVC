using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace BooksApp.Models
{
    public partial class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        [Required]
        [StringLength(250)]
        public string Author { get; set; }
        public int? PublishngYear { get; set; }
        public string Style { get; set; }
    }
}
