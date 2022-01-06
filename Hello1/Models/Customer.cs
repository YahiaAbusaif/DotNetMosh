using Hello1.Models.Validition;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace Hello1.Models
{
    public class Customer
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="please enter valid customer's name")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public MembershipType MembershipType { get; set; }

        //[Range(1,4)]
        [Display(Name ="Membership Type")]
        public Byte MembershipTypeID { get; set; }


        [Min18YearsIfMember]
        [Display(Name ="Date of Birth")]
        public DateTime? BirthDate { get; set; }


        //amount of money the account have
        [Range(-1000000,1000000)]
        public int Money { get; set; } =0 ;



        //add-migration Nameofit
        //update-database


        //Update-Database -TargetMigration:

        //install-package automapper -version:4.1
    }
}