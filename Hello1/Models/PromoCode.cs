using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Hello1.Models
{
    public class PromoCode
    {

        public int ID { get; set; }

        [Required(ErrorMessage =" please enter valid Promo Code")]
        [StringLength(25)]
        [Unique()]
        public String Name { get; set; }

        [Range(1,10000)]
        public int NumberofUse { get; set; }

        [Range(1,100)]
        public int Discount { get; set; }

        [Range(1,10000)]
        public int MaxDiscountInCents { get; set; }


        public MembershipType MembershipRequired { get; set; }
    }


}