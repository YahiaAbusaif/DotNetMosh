using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hello1.Models
{
    public class PricingPolicy
    {

        // divide the pricing based on number of days
        public static readonly int[] MaxNumberOfDays = new int[] { 1, 7, 30, 60, 120 };

        //Precent Price Rate out of 100
        public static readonly int[] PrecentPrice = new int[] { 50, 100, 100, 120, 150 };





    }
}