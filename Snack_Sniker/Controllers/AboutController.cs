using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snack_Sniker.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult About()
        {
            ViewBag.Student1Name = "KANWAL SHEHZADI";
            ViewBag.Student1ID = "02-131212-027";
            ViewBag.Student2Name = "AIMAN ZIA SATTI";
            ViewBag.Student2ID = "02-131212-028";
            ViewBag.ProjectPurpose = "We have created this project of online pizza and drink restaurant to provide a convenient and efficient platform for customers to order their favorite pizzas and drinks from the comfort of their homes. Our aim is to enhance customer satisfaction by offering a wide range of delicious pizzas and refreshing drinks, coupled with a seamless ordering experience.";
            return View();
        }
    }
}
