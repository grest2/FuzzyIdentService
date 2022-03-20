using System.Collections.Generic;
using System.Threading.Tasks;
using FuzzyIdentService.Models.Entities;
using FuzzyIdentService.Utils.Dependency_Injection.Services;
using Microsoft.AspNetCore.Mvc;

namespace FuzzyIdentService.Controllers
{
    [Route("api/import")]
    [ApiController]
    public class ImportController: ControllerBase
    {
        private readonly IImportService _service;

        public ImportController(IImportService service)
        {
            _service = service;
        }

        [HttpPut]
        public async Task<OkResult> ImportUsers(List<User> users)
        {
            await _service.ImportUsers(users);
            return Ok();
        }

        [HttpPatch]
        public async Task<OkResult> UpdateUsers(List<User> users)
        {
            await _service.UpdateUsers(users);
            return Ok();
        }

        [HttpDelete]
        public async Task<OkResult> DeleteUsers(List<User> users)
        {
            await _service.DeleteUsers(users);
            return Ok();
        }
    }
}