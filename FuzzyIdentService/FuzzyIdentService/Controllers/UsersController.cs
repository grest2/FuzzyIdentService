using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzzyIdentService.Abstractions.repo;
using FuzzyIdentService.Fuzzy_Services;
using FuzzyIdentService.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuzzyIdentService.Controllers
{
    [Route("api/[userscontroller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private User user { get; set; }
        private FoneticUser f_user { get; set; }
        private IBaseRepository<FoneticUser> ContextUsers { get; set; }
        private Dictionary<string, int> pick = new Dictionary<string, int>();
        private FuzzyHandlerScope fHandler = new FuzzyHandlerScope();
        private int distance = 3;

        [HttpGet]
        public JsonResult GetUsers(string index)
        {
            return new JsonResult(ContextUsers.Get(index));
        }
        [HttpPost]
        public JsonResult BestMatchLastName(string index, string lastName)
        {
            var users = ContextUsers.Get(index);
            users.ForEach(user => pick.Add(user.FoneticLastName, fHandler
                .BestMatch(lastName, user.FoneticLastName)));

            return new JsonResult(pick.Where(user => user.Value < distance)
                .ToDictionary(element => element.Key,
                element => element.Value));
        }
        [HttpPost]
        public JsonResult BestMatchFirstName(string index, string firstname)
        {
            var users = ContextUsers.Get(index);
            users.ForEach(user => pick.Add(user.FoneticLastName, fHandler
                .BestMatch(firstname, user.FoneticLastName)));

            return new JsonResult(pick.Where(user => user.Value < distance)
                .ToDictionary(element => element.Key,
                element => element.Value));
        }
        [HttpPost]
        public JsonResult BestMatchMiddleName(string index, string middlename)
        {
            var users = ContextUsers.Get(index);
            users.ForEach(user => pick.Add(user.FoneticLastName, fHandler
                .BestMatch(middlename, user.FoneticLastName)));

            return new JsonResult(pick.Where(user => user.Value < distance)
                .ToDictionary(element => element.Key,
                element => element.Value));
        }
    }
}
