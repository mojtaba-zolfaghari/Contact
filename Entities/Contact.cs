using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
   public class Contact 
    {
        public Contact()
        {

            
        }
        public int Id { get; set; }
        [Display(Name ="نام ")]
        public string Name { get; set; }
        [Display(Name = "شماره تلفن همراه ")]
        public string PhoneNumber { get; set; }
        [Display(Name = "شماره تلفن منزل ")]
        public string HomePhone { get; set; }
    }
}
