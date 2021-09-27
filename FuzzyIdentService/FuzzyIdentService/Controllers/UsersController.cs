using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzzyIdentService.Abstractions.repo;
using FuzzyIdentService.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FuzzyIdentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private User user { get; set; }
        private FoneticUser f_user { get; set; }
        private IBaseRepository<FoneticUser> ContextUsers { get; set; }

        [HttpGet]
        public JsonResult GetUsers(string index)
        {
            return new JsonResult(ContextUsers.Get(index));
        }
        [HttpPost]
        public JsonResult BestMatch(string index, string lastName)
        {
           var users = ContextUsers.Get(index);

           var result = users.for
            var fUsers = users.Join(db.FoneticUser,
                user => user.id,
                fUser => fUser.UserId,
                (user, fUser) => new UserFoneticUser
                {
                    user = user,
                    fUser = fUser
                });
            var query = fUsers.OrderBy(fUser => fUser.fUser.FoneticMiddleName)
                .Select(fUser => fUser.fUser.FoneticMiddleName)
                .Distinct();

            await query.ForEachAsync(fUser => pick.Add(fUser, fHandler.BestMatch(LastName, fUser)));

            string[] matchesMiddleNames = pick.Where(element => element.Value < distance)
                .ToDictionary(element => element.Key, element => element.Value)
                .Keys
                .ToArray();

            fUsers = fUsers.Where(fUser => matchesMiddleNames.Contains(fUser.fUser.FoneticMiddleName));
        }
    }
}
