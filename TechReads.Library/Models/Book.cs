using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechReads.Library.Models
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Authors { get; set; }

        [MaxLength(20)]
        public string ISBN { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}
