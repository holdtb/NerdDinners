using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace NerdDinnerFinal.Models
{
    //[Bind(Include="Title,Description,EventDate,Address,Country,HostedBy,ContactPhone,Latitude,Longitude")]
    public partial class Dinner
    {
        public int DinnerId { get; set; }

        [Required(ErrorMessage = "Please enter a Dinner Title")]
        [StringLength(20, ErrorMessage = "Title is too long")]
        public string Title { get; set; }

        [DisplayName("Date")]
        [Required(ErrorMessage = "Please enter the Date of the Dinner")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Please enter the location of the dinner")]
        [StringLength(30, ErrorMessage = "Address is too long")]
        public string Address { get; set; }

        [Required(ErrorMessage = "You must list a phone number")]
        [MinLength(10, ErrorMessage = "Phone number is too short")]
        [MaxLength(14, ErrorMessage = "Phone number is too long")]
        [DisplayName("Phone #")]
        public string ContactPhone { get; set; }

        [DisplayName("Host")]
        [Required(ErrorMessage = "Please enter the host")]
        public string HostedBy { get; set; }

        [Required(ErrorMessage = "You must enter a country")]
        public string Country { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        [Required(ErrorMessage = "Please say what the event is about")]
        [MaxLength(140, ErrorMessage = "The description must be 140 characters or less")]
        public string Description { get; set; }

        public virtual ICollection<RSVP> RSVPs { get; set; }

        public bool IsHostedBy(string userName)
        {
            return HostedBy.Equals(userName, StringComparison.InvariantCultureIgnoreCase);
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (string.IsNullOrEmpty(Title))
                yield return new RuleViolation("Title required", "Title");

            if (string.IsNullOrEmpty(Description))
                yield return new RuleViolation("Description required", "Description");

            if (string.IsNullOrEmpty(HostedBy))
                yield return new RuleViolation("HostedBy required", "HostedBy");

            if (string.IsNullOrEmpty(Address))
                yield return new RuleViolation("Address required", "Address");

            if (string.IsNullOrEmpty(Country))
                yield return new RuleViolation("Country required", "Country");

            if (string.IsNullOrEmpty(ContactPhone))
                yield return new RuleViolation("Phone# required", "ContactPhone");

            if (!PhoneValidator.IsValidNumber(ContactPhone, Country))
                yield return new RuleViolation("Phone# does not match country", "ContactPhone");
        }
    }

   

    public class RuleViolation
    {
        public RuleViolation(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }

        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }
    }

    public class PhoneValidator
    {
        private static readonly IDictionary<string, Regex> countryRegex = new Dictionary<string, Regex>
        {
            {"USA", new Regex("^[2-9]\\d{2}-\\d{3}-\\d{4}$")},
            {
                "UK",
                new Regex(
                    "(^1300\\d{6}$)|(^1800|1900|1902\\d{6}$)|(^0[2|3|7|8]{1}[0-9]{8}$)|(^13\\d{4}$)|(^04\\d{2,3}\\d{6}$)")
            },
            {
                "Netherlands",
                new Regex(
                    "(^\\+[0-9]{2}|^\\+[0-9]{2}\\(0\\)|^\\(\\+[0-9]{2}\\)\\(0\\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\\-\\s]{10}$)")
            }
        };

        public static IEnumerable<string> Countries
        {
            get { return countryRegex.Keys; }
        }

        public static bool IsValidNumber(string phoneNumber, string country)
        {
            if (country != null && countryRegex.ContainsKey(country))
                return countryRegex[country].IsMatch(phoneNumber);
            return false;
        }
    }
}