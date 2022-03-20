using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FuzzyIdentService.Models.Context;
using FuzzyIdentService.Models.Entities;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FuzzyIdentService.Utils.Dependency_Injection.Services
{
    public class ImportService: IImportService
    {
        private readonly ILogger<ImportService> _logger;
        private readonly UserContext _context;

        public ImportService(ILogger<ImportService> logger, UserContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task ImportUsers(List<User> users)
        {
            try
            {
                await _context.UserData.AddRangeAsync(users);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An error occured while add users to db, reason: {0}", ex.Message);
            }
        }

        public async Task UpdateUsers(List<User> users)
        {
            try
            {
                var updateble = await _context.UserData.Where(u => users.Select(d => d.Id).Contains(u.Id))
                    .ToListAsync();
                foreach (var update in updateble)
                {
                    var source = users.FirstOrDefault(u => u.Id.Equals(update.Id));
                    updateble.Remove(update);
                    updateble.Add(source);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("An error occured while to update users, reason: {0}", ex.Message);
            }
        }

        public async Task DeleteUsers(List<User> users)
        {
            try
            {
                _context.UserData.RemoveRange(users);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogWarning("An error while to remove users from db, reason: {0}", e.Message);
            }
        }
    }
}