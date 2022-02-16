using System;
using System.Linq;
using System.Collections.Generic;
using TechReads.Library.Models;

namespace TechReads.Web.Models
{
    public class ReviewerModel
    {
        public ReviewerModel(Reviewer reviewer) => Reviewer = reviewer;
        public Reviewer Reviewer { get; private set; }
    }
}