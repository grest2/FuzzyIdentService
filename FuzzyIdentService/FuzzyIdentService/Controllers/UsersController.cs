
using System.Threading.Tasks;
using FuzzyIdentService.Utils.Dependency_Injection.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace FuzzyIdentService.Controllers
{
    [Route("api/userscontroller")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        public UsersController(IUserService service)
        {
            _service = service;
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
