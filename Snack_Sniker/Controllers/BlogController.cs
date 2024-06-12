using Snack_Sniker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Snack_Sniker.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog
        public ActionResult Index()
        {
            // Sample blog posts - in a real application, retrieve from a database
            var blogPosts = new List<BlogPost>
            {
                new BlogPost { Id = 1, Title = "The Best Pizza Toppings", Content = "Explore the best pizza toppings in the world...", Date = DateTime.Now.AddDays(-5) },
                new BlogPost { Id = 2, Title = "Perfect Drink Pairings", Content = "Discover the perfect drink pairings for your favorite pizzas...", Date = DateTime.Now.AddDays(-3) },
                new BlogPost { Id = 3, Title = "How to Make Homemade Pizza", Content = "A step-by-step guide to making delicious homemade pizza...", Date = DateTime.Now.AddDays(-1) }
            };

            return View(blogPosts);
        }

        // GET: Blog/Details/5
        public ActionResult Details(int id)
        {
            // Sample blog post - in a real application, retrieve from a database
            var blogPosts = new List<BlogPost>
            {
                new BlogPost { Id = 1, Title = "The Best Pizza Toppings", Content = "Explore the best pizza toppings in the world...", Date = DateTime.Now.AddDays(-5) },
                new BlogPost { Id = 2, Title = "Perfect Drink Pairings", Content = "Discover the perfect drink pairings for your favorite pizzas...", Date = DateTime.Now.AddDays(-3) },
                new BlogPost { Id = 3, Title = "How to Make Homemade Pizza", Content = "A step-by-step guide to making delicious homemade pizza...", Date = DateTime.Now.AddDays(-1) }
            };

            var blogPost = blogPosts.FirstOrDefault(p => p.Id == id);
            if (blogPost == null)
            {
                return HttpNotFound();
            }

            return View(blogPost);
        }
    }
}