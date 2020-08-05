using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels.User
{
    public class CreateUserVM
    {

        public long? Id { get; set; } 
        
        [Display(Name ="نام کاربری")]
        [Required]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "نام")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "تلفن")]
        public string Phone { get; set; }
    }
}
