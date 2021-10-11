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
        private IBaseRepository<FoneticUser> ContextUsers { get; set; }
        private Dictionary<string, int> pick = new Dictionary<string, int>();
        private FuzzyHandlerScope fHandler = new FuzzyHandlerScope();
        private int distance = 3;
        private string ErrorReason = "";

        [HttpGet]
        public async Task<JsonResult> GetUser(string index)
        {
            return new JsonResult(await ContextUsers.Get(index));
        }

        [HttpPost]
        public async Task<JsonResult> BestMatchLastName(string index, string lastName)
        {
            var users = await ContextUsers.Get(index);
            users.ForEach(user => pick.Add(user.FoneticLastName, fHandler
                .BestMatch(lastName, user.FoneticLastName)));

            return new JsonResult(pick.Where(user => user.Value < distance)
                .ToDictionary(element => element.Key,
                element => element.Value));
        }
        [HttpPost]
        public async Task<JsonResult> BestMatchFirstName(string index, string firstname)
        {
            var users = await ContextUsers.Get(index);
            users.ForEach(user => pick.Add(user.FoneticLastName, fHandler
                .BestMatch(firstname, user.FoneticLastName)));

            return new JsonResult(pick.Where(user => user.Value < distance)
                .ToDictionary(element => element.Key,
                element => element.Value));
        }
        [HttpPost]
        public async Task<JsonResult> BestMatchMiddleName(string index, string middlename)
        {
            var users = await ContextUsers.Get(index);
            users.ForEach(user => pick.Add(user.FoneticLastName, fHandler
                .BestMatch(middlename, user.FoneticLastName)));

            return new JsonResult(pick.Where(user => user.Value < distance)
                .ToDictionary(element => element.Key,
                element => element.Value));
        }
        // [HttpPut]
        /*public async Task<JsonResult> UpdateUsers(User userUpdated)
        {
            bool success = true;
            
            var user = await ContextUsers.GetSingle(userUpdated.id);
            try
            {
                if (user != null)
                {
                    user = await ContextUsers.Update(userUpdated);
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                ErrorReason = ex.Message;
            }

            return success ? new JsonResult("Update was successful") :
                new JsonResult($"Success is { false.ToString() } because { ErrorReason }");
        }*/
    }
}
