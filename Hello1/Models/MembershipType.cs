using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hello1.Models
{
    public class MembershipType
    {
        public Byte ID { get; set; }
        public short SignupFee { get; set; }
        public Byte DurationInMonths { get; set; }
        public Byte DiscountRate { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public static readonly Byte unknown=0;
        public static readonly Byte Freemembership=1;


    }
}