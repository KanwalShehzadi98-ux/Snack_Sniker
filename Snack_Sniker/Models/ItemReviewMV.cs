using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snack_Sniker.Models
{
    public class ItemReviewMV
    {
        public int Review_Days { get; internal set; }
        public int Rating { get; internal set; }
        public string Review { get; internal set; }
        public string UserPhotoPath { get; internal set; }
        public object ReviewDetails { get; internal set; }
    }
}

