using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FuzzyIdentService.Models.Context;
using FuzzyIdentService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using FuzzyIdentService.Fuzzy_Services;

namespace FuzzyIdentService.Controllers
{
    public class HomeController : Controller
    {
        private UserContext db;
        private FuzzyHandlerScope fHandler = new FuzzyHandlerScope();

        public HomeController(UserContext context)
        {
            db = context;
        }

        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.UserData.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            
            db.UserData.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> FindMatch(string index,string LastName)
        {
            IQueryable<User> users = db.UserData.Where(user=>user.Index == index);
            var fUsers = users.Join(db.FoneticUser,
                user => user.id,
                fUser => fUser.UserId,
                (user, fUser) => new UserFoneticUser
                {
                    user = user,
                    fUser = fUser
                });
            await fUsers.ForEachAsync(fUser => Debug.WriteLine(fHandler.BestMatch(LastName, fUser.fUser.FoneticLastName)));
            return View(await fUsers.ToListAsync());
        }

        
    }
}
