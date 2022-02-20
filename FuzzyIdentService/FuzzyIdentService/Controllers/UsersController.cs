
using System.Threading.Tasks;
using FuzzyIdentService.Models.Entities;
using FuzzyIdentService.Utils.Dependency_Injection.Services.UserService;
using FuzzyIdentService.Utils.Dependency_Injection.Services.UsersManagingService;
using Microsoft.AspNetCore.Mvc;

namespace FuzzyIdentService.Controllers
{
    [Route("api/userscontroller")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IUserManagingService _managing;
        public UsersController(IUserService service, IUserManagingService managing)
        {
            _service = service;
            _managing = managing;
        }

        [HttpPost]
        [Route("get-all")]
        public async Task<JsonResult> GetAll(long count , long offset)
        {
            return new JsonResult(await _managing.GetAllUsers(count, offset));
        }

        [HttpPost]
        [Route("get-by-index")]
        public async Task<JsonResult> GetUserByIndex(string index, long count, long offset)
        {
            return new JsonResult(await _managing.GetUsersByIndex(index, count, offset));
        }

        [HttpPut]
        [Route("create-user")]
        public async Task<JsonResult> CreateUser(User user)
        {
            return new JsonResult(await _managing.AddUser(user));
        }

        [HttpPost]
        [Route("users/best-match-lastname")]
        public async Task<JsonResult> BestMatchLastName(string index, string lastName, long count, long offset)
        {
            return new JsonResult(await _service.BestLastNames(index, lastName, count, offset));
        }
        
        [HttpPost]
        [Route("users/best-match-firstname")]
        public async Task<JsonResult> BestMatchFirstName(string index, string firstName, long count, long offset)
        {
            return new JsonResult(await _service.BestFirstName(index, firstName, count, offset));
        }
        
        [HttpPost]
        [Route("users/best-match-middlename")]
        public async Task<JsonResult> BestMatchMiddleName(string index, string middleName, long count, long offset)
        {
            return new JsonResult(await _service.BestMiddleName(index, middleName, count, offset));
        }
    }
}
