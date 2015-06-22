using System.Web.Mvc;

namespace NerdDinnerFinal.Models.ViewModels
{
    public class DinnerFormViewModel
    {
        public DinnerFormViewModel(Dinner dinner)
        {
            Dinner = dinner;
            Countries = new SelectList(PhoneValidator.Countries, dinner.Country);
        }

        public Dinner Dinner { get; set; }
        public SelectList Countries { get; set; }
    }
}