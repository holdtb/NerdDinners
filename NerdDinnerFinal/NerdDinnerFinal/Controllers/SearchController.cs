using System;
using System.Linq;
using System.Web.Mvc;
using NerdDinnerFinal.Models;

namespace NerdDinnerFinal.Controllers
{
    public class JsonDinner
    {
        public int DinnerID { get; set; }
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Description { get; set; }
        public int RSVPCount { get; set; }
    }

    public class SearchController : Controller
    {
        DinnersRepository dinnerRepository = new DinnersRepository();
//
// AJAX: /Search/SearchByLocation
        /*
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult SearchByLocation(float longitude, float latitude)
        {
            var dinners = dinnerRepository.FindByLocation(latitude, longitude);

            var jsonDinners = from dinner in dinners
                select new JsonDinner
                {
                    DinnerID = dinner.DinnerId,
                    Latitude = Convert.ToDouble(dinner.Latitude),
                    Longitude = Convert.ToDouble(dinner.Longitude),
                    Title = dinner.Title,
                    Description = dinner.Description,
                    RSVPCount = dinner.RSVPs.Count
                };

            return Json(jsonDinners.ToList());
        }*/
    }
         
}