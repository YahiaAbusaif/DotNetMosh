using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hello1.Models
{
    public class MembershipInfo
    {
        enum Membership
        {
 
            unknown=0,
            Freemembership=1,
            Paidmembership=2,
            Promembership=3,
            Ultimatemembership=4
 
        }

         //Signup Fee In Cents for 
        public static readonly int[] SignupFeeInCents = new int[] { 0, 0, 4000, 6000, 10000 };

        //
        public static readonly byte[] DurationInMonths = new byte[] { 0, 0, 12, 12, 12 };

         //discount Rate out of 100
        public static readonly int[] DiscountRate = new int[] { 0, 0, 15, 30, 70 };
        //Max Dept amount for each membeship
        public static readonly int[] MaxDeptForAccountInCents = new int[] { 0, 0, -10000, -100000, -1000000 };


    }
}