using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Repository.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DataBaseContext _context;

        public GenericRepository(DataBaseContext context)
        {
            _context = context;

        }


        public virtual T Insert(T entity)
        {
            _context.Set<T>().Add(entity);
            return entity;
        }

        public void Delete(T t)
        {
            _context.Set<T>().Remove(t);
            _context.SaveChanges();
        }

        public T Update(T updated)
        {
            if (updated == null)
            {
                return null;
            }

            _context.Set<T>().Update(updated);

            _context.SaveChanges();

            return updated;
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).AsEnumerable();
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Where(match).ToList();
        }
        public ICollection<T> GetAll()
        {

            try
            {
                return _context.Set<T>().ToList();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                throw ex;
            }

        }
    }
}
