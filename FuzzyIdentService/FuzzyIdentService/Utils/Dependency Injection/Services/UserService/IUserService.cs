using System.Collections.Generic;
using System.Threading.Tasks;
using FuzzyIdentService.Models;

namespace FuzzyIdentService.Utils.Dependency_Injection.Services.UserService
{
    public interface IUserService
    {
        Task<Row<string>> BestLastNames(string index, string lastName, long count, long offset);
        Task<Row<string>> BestFirstName(string index, string firstName, long count, long offset);
        Task<Row<string>> BestMiddleName(string index, string middleName, long count, long offset);
    }
}