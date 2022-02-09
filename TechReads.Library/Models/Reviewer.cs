using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechReads.Library.Models
{
    public class Reviewer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewerId { get; set; }
        [Required]
        public string UserName { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public List<Review> Reviews { get; set; }
    }
}