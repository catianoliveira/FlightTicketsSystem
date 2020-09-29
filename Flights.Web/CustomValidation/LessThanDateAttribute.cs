using System;
using System.ComponentModel.DataAnnotations;

namespace FlightTicketsSystem.Web.CustomValidation
{
    public class LessThanDateAttribute : ValidationAttribute
    {
        /// <summary>
        /// checks if date is lesser than todays
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            DateTime date = (DateTime)value;
            return date < DateTime.UtcNow;
        }
    }
}
