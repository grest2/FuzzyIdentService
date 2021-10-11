using FuzzyIdentService.Abstractions.repo;
using FuzzyIdentService.Models.Context;
using FuzzyIdentService.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FuzzyIdentService.Utils.Dependency_Injection
{
    public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseUser
    {
        private UserContext Context { get; set; }

        public BaseRepository(UserContext context)
        {
            this.Context = context;
        }

        public async Task<TDbModel> Create(TDbModel model)
        {
            await Context.Set<TDbModel>().AddAsync(model);
            await Context.SaveChangesAsync();
            return model;
        }

        public async Task<TDbModel> GetSingle(string id)
        {
            return await Context.Set<TDbModel>()
                .FirstOrDefaultAsync(user => user.id == id);
        }

        public async Task Delete(string id)
        {
            var toDelete = await Context.Set<TDbModel>()
                .FirstOrDefaultAsync(user => user.id == id);
            Context.Set<TDbModel>().Remove(toDelete);
            await Context.SaveChangesAsync();
        }

        public async Task<List<TDbModel>> Get(string index)
        {
            return await Context.Set<TDbModel>()
                .Where(model => model.id == index).ToListAsync();
        }

        public async Task<List<TDbModel>> GetAll()
        {
            return await Context.Set<TDbModel>()
                .ToListAsync();
        }

        public async Task<TDbModel> Update(TDbModel model)
        {
            var toUpdate = await Context.Set<TDbModel>()
                .FirstOrDefaultAsync(user => user.id == model.id);
            if (toUpdate != null)
            {
                toUpdate = model;
            }

            if (toUpdate != null)
            {
                Context.Update(toUpdate);
                await Context.SaveChangesAsync();
                return toUpdate;
            }

            return null;
        }
    }
}
