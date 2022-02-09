using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechReads.Library.Models
{
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int ReviewerId { get; set; }

        [Range(1, 5)]
        public int Stars { get; set; }

        [Column(TypeName="ntext")]
        public string Comment { get; set; }
    }

}