using Hello1.Models.Validition;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hello1.Dtos
{
    public class CustomerDto
    {
         public int ID { get; set; }
        [Required(ErrorMessage ="please enter valid customer's name")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }

        //[Range(1,4)]
        public Byte MembershipTypeID { get; set; }


        [Min18YearsIfMember]
        public DateTime? BirthDate { get; set; }
    }
}