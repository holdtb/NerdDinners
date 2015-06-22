using NerdDinnerFinal.Models;

namespace NerdDinnerFinal.Models
{
    public class RSVP
    {
        public int RsvpId { get; set; }
        public int DinnerId { get; set; }
        public string AttendeeEmail { get; set; }
        public virtual Dinner Dinner { get; set; }
    }
}