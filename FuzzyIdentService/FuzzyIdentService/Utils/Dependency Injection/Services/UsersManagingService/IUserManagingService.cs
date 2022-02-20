using System.Threading.Tasks;
using FuzzyIdentService.Models;
using FuzzyIdentService.Models.Entities;

namespace FuzzyIdentService.Utils.Dependency_Injection.Services.UsersManagingService
{
    public interface IUserManagingService
    {
        public Task<Row<User>> GetUsersByIndex(string index, long count, long offset);
        public Task<Row<User>> GetAllUsers(long count, long offset);
        public Task<User> AddUser(User user);
    }
}