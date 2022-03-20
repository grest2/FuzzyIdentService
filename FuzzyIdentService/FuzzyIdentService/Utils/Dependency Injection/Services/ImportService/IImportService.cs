using System.Collections.Generic;
using System.Threading.Tasks;
using FuzzyIdentService.Models.Entities;

namespace FuzzyIdentService.Utils.Dependency_Injection.Services
{
    public interface IImportService
    {
        public Task ImportUsers(List<User> users);
        public Task UpdateUsers(List<User> users);
        public Task DeleteUsers(List<User> users);
    }
}