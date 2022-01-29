using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Repo
{
    public interface IGenericRepository<T>
    {
        T Add(T model);
        void Update(T model);
        IQueryable<T> GetAll();
        T GetById(int id);
        int Commit();
    }
}
