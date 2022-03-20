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

        public T GetById(Expression<Func<T, bool>> filter)
        {
            var dbResult = dbSet.Where(filter).SingleOrDefault();
            return dbResult;
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.AsEnumerable();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter).AsEnumerable();
        }

        public virtual T Insert(T entity)
        {

            dynamic obj = dbSet.Add(entity);
            this._unitOfWork.Db.SaveChanges();
            return obj;

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

        public void Delete(Expression<Func<T, bool>> filter)
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
        public bool Exist(Expression<Func<T, bool>> filter)
        {
            return dbSet.Any(filter);
        }
        
        public int Count(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter).Count();
        }

      
    }

}
