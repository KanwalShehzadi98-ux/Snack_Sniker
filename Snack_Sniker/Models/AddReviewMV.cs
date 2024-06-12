using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snack_Sniker.Models
{
    public class AddReviewMV
    {
        public int Review_Days { get; set; }
        public string ReviewBy_User { get; set; }
        public string UserPhotoPath { get; set; }
        public int Rating { get; set; }
        public string ReviewDetails { get; set; }
    }
}