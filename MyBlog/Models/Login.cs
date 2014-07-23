using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //using for data annotations

namespace MyBlog.Models
{
    public class Login
    {
        [Required, Display(Name="User Name")]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

    }
}