using System;
using System.Linq;
using System.Collections.Generic;
using TechReads.Library.Models;

namespace TechReads.Web.Models
{
    public class ReviewerListModel
    {
        public ReviewerListModel(IEnumerable<Reviewer> reviewers) => Reviewers = reviewers;
        public IEnumerable<Reviewer> Reviewers { get; private set; }
    }
}