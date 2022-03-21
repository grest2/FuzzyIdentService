using System.Linq;
using System.Threading.Tasks;
using FuzzyIdentService.Extensions;
using FuzzyIdentService.Models;
using FuzzyIdentService.Models.Context;
using FuzzyIdentService.Models.Entities;

namespace FuzzyIdentService.Utils.Dependency_Injection.Services.UsersManagingService
{
    public class UserManagingService: IUserManagingService
    {
        private readonly UserContext _ctx;

        public UserManagingService(UserContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Row<User>> GetUsersByIndex(string index, long count, long offset)
        {
            var users = await _ctx.UserData.Where(u => u.Index.Equals(index)).ToRowAsync(count, offset);
            return users;
        }

        public async Task<Row<User>> GetAllUsers(long count, long offset)
        {
            return await _ctx.UserData.ToRowAsync(count, offset);
        }

        public async Task<User> AddUser(User user)
        {
            await _ctx.UserData.AddAsync(user);
            await _ctx.SaveChangesAsync();

            return user;
        }
    }
}