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
using FuzzyIdentService.Utils.Dependency_Injection.Services.UserService;
using FuzzyIdentService.Utils.Dependency_Injection.Services.UsersManagingService;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FuzzyIdentService.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _service;
        private readonly IUserManagingService _managing;
        public HomeController(IUserService service, IUserManagingService managing)
        {
            _service = service;
            _managing = managing;
        }

        public IActionResult Find()
        {
            return View();
        }
        public async Task<IActionResult> Index(long count, long offset)
        {
            //return View(await db.UserData.ToListAsync());
            return View(await _managing.GetAllUsers(count, offset));
        }
        public IActionResult CreateUser()
        {
            return View();
        }

        public async Task<IActionResult> FindMatch(string index,string lastName)
        {
            var users = await _service.BestLastNames(index, lastName, 0, 10);
            return View( users );
        }
    }
}
