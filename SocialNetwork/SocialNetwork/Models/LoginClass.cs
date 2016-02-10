using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.Models
{
    public class LoginClass
    {
        [Required(ErrorMessage = "Логин не введен")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль не введен")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}