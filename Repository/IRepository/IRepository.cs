using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Wales_Online_Bank.Repository.IRepository
{
    public interface IRepository<T> where T : class 
    {
        IEnumerable<T> GetAllAccOrCustomerUser();
        T GetAccOrCustomerUser(Expression<Func<T, bool>> filter);
        void CreateAccOrCustomerUser(T entity);
          
        void DeleteAccOrCustomerUser(T entity); 
        
        void UpdateAccOrCustomerUser(T entity);
    }
    
    
}
