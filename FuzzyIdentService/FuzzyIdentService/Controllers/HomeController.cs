using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FuzzyIdentService.Models;
using FuzzyIdentService.Models.Context;
using FuzzyIdentService.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuzzyIdentService.Controllers
{
    public class HomeController : Controller
    {
        private UserContext db;

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
    }
}
