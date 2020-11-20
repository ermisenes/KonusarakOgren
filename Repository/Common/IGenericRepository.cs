using Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Repository.Common
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        ICollection<T> GetAll();
        ICollection<T> FindAll(Expression<Func<T, bool>> predicate); // LINQ desteği sunabilmek i
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Insert(T entity);
        void Delete(T entity);
        T Update(T updated);
       
    }
}
