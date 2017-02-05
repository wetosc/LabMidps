using System;
using System.Collections.Generic;

namespace LAB7.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? CanViewExtra { get; set; }
        public bool? CanEdit { get; set; }
    }
}
