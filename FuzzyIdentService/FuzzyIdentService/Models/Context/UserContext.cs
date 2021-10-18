using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzzyIdentService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuzzyIdentService.Models.Context
{
   public class UserContext : DbContext
   {
        public UserContext(DbContextOptions<UserContext> options) : base(options) { /*Database.EnsureCreated(); */}
        public virtual DbSet<User> UserData { get; set; }
        public virtual DbSet<FoneticUser> FoneticUser { get; set; }
   }
}
