using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzzyIdentService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuzzyIdentService.Models.Context
{
     class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public virtual DbSet<User> User { get; set; }
    }
}
