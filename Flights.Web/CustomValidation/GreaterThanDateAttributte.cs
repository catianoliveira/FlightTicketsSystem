﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FlightTicketsSystem.Web.CustomValidation
{
    public class GreaterThanDateAttributte : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime date = (DateTime)value;
            return date > DateTime.UtcNow;
        }
    }
}
