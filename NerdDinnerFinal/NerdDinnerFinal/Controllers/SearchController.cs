using System;
using System.Linq;
using System.Web.Mvc;
using NerdDinnerFinal.Models;

namespace NerdDinnerFinal.Controllers
{
    public class JsonDinner
    {
        public int DinnerId { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public int RsvpCount { get; set; }
    }

    public class SearchController : Controller
    {
        private NerdDinnersDbContext db = new NerdDinnersDbContext();

        //
        // AJAX: /Search/SearchByLocation
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SearchByLocation(float longitude, float latitude)
        {
            var dinners = db.FindByLocation(latitude, longitude);

            var jsonDinners = from dinner in dinners
                              select new JsonDinner {
                                  DinnerId = dinner.DinnerId,
                                  Latitude =  dinner.Latitude,
                                  Longitude = dinner.Longitude,
                                  Title = dinner.Title,
                                  Description = dinner.Description,
                                  RsvpCount = dinner.RSVPs.Count
                              };

            var results = jsonDinners.ToList();
            return Json(results);
        }
    }
}