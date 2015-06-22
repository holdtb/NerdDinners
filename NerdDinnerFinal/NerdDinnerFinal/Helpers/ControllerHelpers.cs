using System.Collections.Generic;
using System.Web.Mvc;
using NerdDinnerFinal.Models;

namespace NerdDinnerFinal.Helpers
{
    public static class ControllerHelpers
    {
        /*
         * Adds errors to the modelstate
         * --Removes need for validation in controller or view
         */

        public static void AddRuleViolations(this ModelStateDictionary modelState, IEnumerable<RuleViolation> errors)
        {
            foreach (var issue in errors)
            {
                modelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
            }
        }
    }
}