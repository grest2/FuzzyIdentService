﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Models.Entities
{
    public class UserFoneticUser
    {
        public User user { get; set; }
        public FoneticUser fUser { get; set; }
    }
}
