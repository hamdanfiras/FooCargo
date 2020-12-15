using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FooCargo.CoreModels
{
    public class RegisterInfo
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
       
        [Required]
        public string FullName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
