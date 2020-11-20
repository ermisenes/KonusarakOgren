using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Common
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;

       int Commit();

        void Rollback();
    }
}
