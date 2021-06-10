using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FuzzyIdentService.Models.Context;
using FuzzyIdentService.Models.Entities;
using Microsoft.EntityFrameworkCore;
using FuzzyIdentService.Fuzzy_Services;
using System.Diagnostics;

namespace FuzzyIdentService.Controllers
{
    public class HomeController : Controller
    {
        private UserContext db;
        private FuzzyHandlerScope fHandler = new FuzzyHandlerScope();
        private Dictionary<string, int> pick = new Dictionary<string, int>();
        private int distance = 3;

        public HomeController(UserContext context)
        {
            db = context;
        }

        public IActionResult Find()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.UserData.ToListAsync());
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(string ID,string FirstName,string MiddleName,string LastName,string Index)
        {
            User user = new User(ID, FirstName, MiddleName, LastName, Index);
            FoneticUser foneticUser = new FoneticUser(ID,
                RussianMetaphone.getInstance().getRightName(user.FirstName),
                RussianMetaphone.getInstance().getRightName(user.MiddleName),
                RussianMetaphone.getInstance().getRightName(user.LastName),
                ID);
            db.UserData.Add(user);
            db.FoneticUser.Add(foneticUser);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

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
            var query = fUsers.OrderBy(fUser => fUser.fUser.FoneticMiddleName).Select(fUser=>fUser.fUser.FoneticMiddleName).Distinct();
            await query.ForEachAsync(fUser =>pick.Add(fUser, fHandler.BestMatch(LastName, fUser)));
            string[] matchesMiddleNames = pick.Where(element => element.Value < distance).ToDictionary(element => element.Key,element => element.Value).Keys.ToArray();
            fUsers = fUsers.Where(fUser => matchesMiddleNames.Contains(fUser.fUser.FoneticMiddleName));
            
            return View(await fUsers.ToListAsync());
        }

        
    }
}
