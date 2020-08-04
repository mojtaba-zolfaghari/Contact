﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class UserProfile : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public virtual User User { get; set; }
    }
}
