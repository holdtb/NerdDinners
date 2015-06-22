using System;
using System.Linq;
using System.Web.Mvc;
using NerdDinnerFinal.Models;

namespace NerdDinnerFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly NerdDinnersDbContext nerdDinnersDbContext = new NerdDinnersDbContext();
        // GET: Home
        public ActionResult Index()
        {
            var dinners = from d in nerdDinnersDbContext.Dinners
                where d.EventDate > DateTime.Now
                select d;

            return View(dinners.ToList());
        }

        //
        //GET: /Home/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        //POST: /Home/Create
        [HttpPost]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                nerdDinnersDbContext.Dinners.Add(dinner);
                nerdDinnersDbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(dinner);
        }
    }
}