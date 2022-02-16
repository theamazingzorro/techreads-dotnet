using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechReads.BusinessLogic.Filters
{
    public class ReviewFilter
    {
        public int MinRating { get; set; } = 0;
        public int MaxRating { get; set; } = 5;
    }
}
