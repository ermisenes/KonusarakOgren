using Model;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    
     public interface IQuestionRepository : IGenericRepository<Question>
    {
        Question GetById(int id);
        bool CheckQuestionName(string CountyName);
    }
}
