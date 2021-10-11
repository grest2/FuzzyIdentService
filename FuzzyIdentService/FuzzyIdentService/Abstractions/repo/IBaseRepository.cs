using FuzzyIdentService.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Abstractions.repo
{
    interface IBaseRepository<TDbModel> where TDbModel : BaseUser
    {
        public Task<List<TDbModel>> GetAll();
        public Task<TDbModel> GetSingle(string id);
        public Task<List<TDbModel>> Get(string index);
        public Task<TDbModel> Update(TDbModel model);

        public Task Delete(string id);

    }
}
