using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechReads.BusinessLogic.Filters
{
    public enum ReviewOrdering
    {
        None,
        [Display(Name = "Rating ↑")]RatingAsc,
        [Display(Name = "Rating ↓")] RatingDesc,
        Username
    }
}
