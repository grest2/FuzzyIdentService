using FuzzyIdentService.Abstractions.repo;
using FuzzyIdentService.Models.Context;
using FuzzyIdentService.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Utils.Dependency_Injection
{
    public class BaseRepository<TDbModel> : IBaseRepository<TDbModel> where TDbModel : BaseUser
    {
        private UserContext Context { get; set; }

        public BaseRepository(UserContext context)
        {
            this.Context = context;
        }

        public TDbModel Create(TDbModel model)
        {
            Context.Set<TDbModel>().Add(model);
            Context.SaveChanges();
            return model;
        }

        public void Delete(string id)
        {
            var toDelete = Context.Set<TDbModel>().FirstOrDefault(user => user.id == id);
            Context.Set<TDbModel>().Remove(toDelete);
            Context.SaveChanges();
        }

        public List<TDbModel> Get(string index)
        {
            return Context.Set<TDbModel>().ToList().FindAll(user => user.Index == index);
        }

        public List<TDbModel> GetAll()
        {
            return Context.Set<TDbModel>().ToList();
        }

        public TDbModel Update(TDbModel model)
        {
            var toUpdate = Context.Set<TDbModel>().FirstOrDefault(model => model.id == model.id);
            if (toUpdate != null)
            {
                toUpdate = model;
            }
            Context.Update(toUpdate);
            Context.SaveChanges();
            return toUpdate;
        }
    }
}
