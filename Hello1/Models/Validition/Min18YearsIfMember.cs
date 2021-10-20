using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hello1.Models.Validition
{
    public class Min18YearsIfMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Customer) validationContext.ObjectInstance;

            if(customer.MembershipTypeID == MembershipType.unknown || 
                customer.MembershipTypeID== MembershipType.Freemembership)
                return ValidationResult.Success;

            if(customer.BirthDate==null)
                return new ValidationResult("Birth Date is required");

            var age= DateTime.Today.Year -customer.BirthDate.Value.Year;

            if(age<18)
                 return new ValidationResult("this Membership is for user with 18 years old or more ");


            return ValidationResult.Success;
        }
    }
}