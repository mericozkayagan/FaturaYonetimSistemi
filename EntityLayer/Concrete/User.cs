﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; }        
        public string Surname { get; set; }        
        public string TCNo { get; set; }
        public string  Email { get; set; }
        public string?  Password { get; set; }
        public string  PhoneNumber { get; set; }
        public string?  CarStatus { get; set; }
        public bool IsActive { get; set; }

        
    }
}
