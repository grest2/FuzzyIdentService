using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzzyIdentService.Abstractions;
using FuzzyIdentService.Abstractions.repo;
using FuzzyIdentService.Extensions;
using FuzzyIdentService.Fuzzy_Services;
using FuzzyIdentService.Models;
using FuzzyIdentService.Models.Context;
using FuzzyIdentService.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace FuzzyIdentService.Utils.Dependency_Injection.Services.UserService
{
    public class UserService: IUserService
    {
        private readonly UserContext _ctx;
        private readonly IFuzzyHandler _fuzzyHandler;
        
        private int distance = 3;

        public UserService(UserContext ctx, IFuzzyHandler handler)
        {
            _ctx = ctx;
            _fuzzyHandler = handler;
        }
        public async Task<Row<string>> BestLastNames(string index, string lastName, long count, long offset)
        {
            var users = await _ctx.UserData.Where(u => u.Index.Equals(index))
                .Where(u => _fuzzyHandler.BestMatch(lastName, u.LastName) < distance)
                .Select(u => _fuzzyHandler.GetFoneticKey(u.LastName)).ToRowAsync(count, offset);
            
            return users;
        }

        public async Task<Row<string>> BestFirstName(string index, string firstName, long count, long offset)
        {
            var users = await _ctx.UserData.Where(u => u.Index.Equals(index))
                .Where(u => _fuzzyHandler.BestMatch(firstName, u.FirstName) < distance)
                .Select(u => _fuzzyHandler.GetFoneticKey(u.FirstName)).ToRowAsync(count, offset);
            
            return users;
        }

        public async Task<Row<string>> BestMiddleName(string index, string middleName, long count, long offset)
        {
            var users = await _ctx.UserData.Where(u => u.Index.Equals(index))
                .Where(u => _fuzzyHandler.BestMatch(middleName, u.MiddleName) < distance)
                .Select(u => _fuzzyHandler.GetFoneticKey(u.MiddleName)).ToRowAsync(count, offset);
            
            return users;
        }
    }
}