using System;
using System.Web.Mvc;
using NerdDinnerFinal.Helpers;
using NerdDinnerFinal.Models;
using NerdDinnerFinal.Models.ViewModels;
using System.Web.UI.WebControls;

namespace NerdDinnerFinal.Controllers
{
    public class DinnersController : Controller
    {
        private IDinnersRepository dinnerRepository;


        public DinnersController()
            : this(new DinnersRepository())
        {
            
        }

        public DinnersController(IDinnersRepository repository)
        {
            dinnerRepository = repository;
        }


        // GET: /Dinners/
        //      /Dinners?page=2
        public ActionResult Index(int? page)
        {
            const int pageSize = 10;

            var upcomingDinners = dinnerRepository.FindUpcomingDinners();
            var paginatedDinners = new PaginatedList<Dinner>(upcomingDinners, page ?? 0, pageSize);

            return View(paginatedDinners);
        }

        // GET: /Dinners/Details/5
        public ActionResult Details(int id)
        {
            var dinner = dinnerRepository.GetDinner(id);

            if(dinner == null)
                return View("NotFound");
            
            return View(dinner);
        }

        //GET: /Dinners/Edit/2
        public ActionResult Edit(int id)
        {
            var dinner = dinnerRepository.GetDinner(id);

            if (!dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            return View(new DinnerFormViewModel(dinner));
        }

        //POST: /Dinners/Edit/2
        [HttpPost]
        public ActionResult Edit(int id, FormCollection formValues)
        {
            var dinner = dinnerRepository.GetDinner(id);

            if(!dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            try
            {
                UpdateModel(dinner, "Dinner");
                dinnerRepository.Save();

                return RedirectToAction("Details", new {id = dinner.DinnerId});
            }
            catch
            {
                ModelState.AddRuleViolations(dinner.GetRuleViolations());
                return View(new DinnerFormViewModel(dinner));
            }
        }

        //GET: /Dinners/Create
        [Authorize]
        public ActionResult Create()
        {
            var dinner = new Dinner
            {
                EventDate = DateTime.Now.AddDays(7)
            };
            return View(new DinnerFormViewModel(dinner));
        }

        //POST: /Dinners/Create
        [HttpPost, Authorize]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dinner.HostedBy = User.Identity.Name;

                    RSVP rsvp = new RSVP();
                    rsvp.AttendeeEmail = User.Identity.Name;
                    dinner.RSVPs.Add(rsvp);

                    dinnerRepository.Add(dinner);
                    dinnerRepository.Save();

                    return RedirectToAction("Details", new {id = dinner.DinnerId});
                }
                catch
                {
                    ModelState.AddRuleViolations(dinner.GetRuleViolations());
                }
            }

            return View(new DinnerFormViewModel(dinner));
        }

        //GET: /Dinners/Delete/1
        public ActionResult Delete(int id)
        {
            var dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");

            if (dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");
            return View(dinner);
        }

        //POST: /Dinners/Delete/1
        [HttpPost]
        public ActionResult Delete(int id, string confirmButton)
        {
            var dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");

            if (dinner.IsHostedBy(User.Identity.Name))
                return View("InvalidOwner");

            dinnerRepository.Delete(dinner);
            dinnerRepository.Save();

            return View("Deleted");
        }
    }
}