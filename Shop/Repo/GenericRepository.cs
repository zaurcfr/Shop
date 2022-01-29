using Microsoft.EntityFrameworkCore;
using Shop.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Repo
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext context;
        
        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public T Add(T model)
        {
            context.Entry<T>(model).State = EntityState.Added;
            return model;
        }

        public int Commit()
        {
            var result = context.SaveChanges();
            return result;
        }

        public IQueryable<T> GetAll()
        {
            var data = context.Set<T>();
            return data;
        }

        public T GetById(int id)
        {
            var data = context.Set<T>().Find(id);
            return data;
        }

        public void Update(T model)
        {
            context.Entry<T>(model).State = EntityState.Modified;
        }
    }
}
