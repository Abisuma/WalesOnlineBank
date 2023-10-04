using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using Wales_Online_Bank.Data;
using Wales_Online_Bank.Repository.IRepository;


namespace Wales_Online_Bank.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext db;
        internal DbSet<T> dbSet;

        public Repository(AppDbContext dbContext)
        {
            db = dbContext;
            this.dbSet = db.Set<T>();
        }
        public void CreateAccOrCustomerUser(T entity)
        {
            dbSet.Add(entity);
        }

        public void DeleteAccOrCustomerUser(T entity)
        {
            dbSet.Remove(entity);
        }

        public T GetAccOrCustomerUser(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            return query.FirstOrDefault();
        }

       public IEnumerable<T> GetAllAccOrCustomerUser()
       {
            IQueryable<T> query = dbSet;
            return query.ToList();
       }

        
    }
}
