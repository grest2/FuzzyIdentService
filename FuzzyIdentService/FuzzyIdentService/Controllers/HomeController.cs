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
using FuzzyIdentService.Abstractions.repo;
using FuzzyIdentService.Utils.Dependency_Injection;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FuzzyIdentService.Controllers
{
    public class HomeController : Controller
    {
        private UserContext db;
        private FuzzyHandlerScope fHandler = new FuzzyHandlerScope();
        private Dictionary<string, int> pick = new Dictionary<string, int>();
        private int distance = 3;
        
        private IBaseRepository<FoneticUser> ContextUsersFonetic { get; set; }
        private IBaseRepository<User> UserContext { get; set; }

        public HomeController(UserContext context)
        {
            db = context;
            this.ContextUsersFonetic = new BaseRepository<FoneticUser>(context);
            this.UserContext = new BaseRepository<User>(context);
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
                RussianMetaphone.getInstance()
                .getRightName(user.FirstName),
                RussianMetaphone.getInstance()
                .getRightName(user.MiddleName),
                RussianMetaphone.getInstance()
                .getRightName(user.LastName),
                ID);
            db.UserData.Add(user);
            db.FoneticUser.Add(foneticUser);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> FindMatch(string index,string LastName)
        {
            var users = await UserContext.Get(index);

            var unicUsers = users.OrderBy(user => user.MiddleName)
                .Select(user => user.MiddleName)
                .Distinct();
            
            foreach (var lastNameUser in unicUsers)
            {
                pick.Add(lastNameUser,fHandler.BestMatch(LastName,lastNameUser));
            }

            IEnumerable<String> result = pick.Where(user => user.Value < distance)
                .ToDictionary(element => element.Key,
                    element => element.Value).Keys.ToArray();
            return View( result );
        }

        
    }
}
