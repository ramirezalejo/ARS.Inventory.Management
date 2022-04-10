using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ARS.Inventory.Management.Infrastructure.Repository.Context
{
    public class BaseRepository<T> : IBaseRespository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        internal DbSet<T> dbSet;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException("unitOfWork");
            _unitOfWork = unitOfWork;
            this.dbSet = _unitOfWork.Db.Set<T>();
        }

        public virtual T GetById(Expression<Func<T, bool>> filter)
        {
            var dbResult = dbSet.Where(filter).SingleOrDefault();
            return dbResult;
        }

        public async virtual Task<T> GetByIdAsync(Expression<Func<T, bool>> filter)
        {
            var dbResult = await dbSet.FirstAsync(filter);
            return dbResult;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable();
        }

        public virtual IEnumerable<T> GetAllPaginated(int page, int pageSize)
        {

            return dbSet.AsEnumerable().Skip(page * pageSize).Take(pageSize);
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter).AsEnumerable();
        }

        public virtual void Insert(T entity)
        {

            dbSet.Add(entity);
            this._unitOfWork.Db.SaveChanges();

        }

        public virtual async Task InsertAsync(T entity)
        {

            await dbSet.AddAsync(entity);
            this._unitOfWork.Db.SaveChanges();

        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            _unitOfWork.Db.Entry(entity).State = EntityState.Modified;
            this._unitOfWork.Db.SaveChanges();
        }

        public virtual void UpdateAll(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                dbSet.Attach(entity);
                _unitOfWork.Db.Entry(entity).State = EntityState.Modified;
            }
            this._unitOfWork.Db.SaveChanges();
        }

        public virtual void Delete(Expression<Func<T, bool>> filter)
        {
            IEnumerable<T> entities = this.GetAll(filter);
            foreach (T entity in entities)
            {
                if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbSet.Remove(entity);
            }
            this._unitOfWork.Db.SaveChanges();
        }

        // ------- Extra Methods ------ ///
        public virtual bool Exist(Expression<Func<T, bool>> filter)
        {
            return dbSet.Any(filter);
        }

        public virtual int Count(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter).Count();
        }

      
    }

}
