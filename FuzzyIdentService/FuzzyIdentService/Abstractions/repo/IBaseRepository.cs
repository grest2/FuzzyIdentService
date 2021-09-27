using FuzzyIdentService.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FuzzyIdentService.Abstractions.repo
{
    interface IBaseRepository<TDbModel> where TDbModel : BaseUser
    {
        public List<TDbModel> GetAll();
        public List<TDbModel> Get(string index);
        public TDbModel Create(TDbModel model);
        public TDbModel Update(TDbModel model);
        public void Delete(String id);

    }
}
