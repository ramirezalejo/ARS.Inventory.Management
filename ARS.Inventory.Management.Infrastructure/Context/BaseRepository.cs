﻿using ARS.Inventory.Management.Domain.Interfaces;
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

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter)
        {
            return dbSet.Where(filter).AsEnumerable();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await dbSet.Where(filter).ToListAsync();
        }
        public virtual async Task<IEnumerable<T>> GetPagedAsync(Expression<Func<T, bool>> filter, int skip)
        {
            return await dbSet.Where(filter).Skip(skip).ToListAsync();
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<T, bool>> filter)
        {
            return await dbSet.CountAsync(filter);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual void Insert(T entity)
        {

            dbSet.Add(entity);
            this._unitOfWork.Db.SaveChanges();

        }

        public virtual void InsertAll(IList<T> entities)
        {

            foreach (var entity in entities)
            {
                dbSet.AddAsync(entity);
            }
            this._unitOfWork.Db.SaveChanges();

        }

        public virtual async Task InsertAsync(T entity)
        {

            await dbSet.AddAsync(entity);
            this._unitOfWork.Db.SaveChanges();

        }

        public virtual Task InsertAllAsync(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                dbSet.AddAsync(entity);
            }
            return this._unitOfWork.Db.SaveChangesAsync();
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            _unitOfWork.Db.Entry(entity).State = EntityState.Modified;
            this._unitOfWork.Db.SaveChanges();
        }

        public virtual Task UpdateAsync(T entity)
        {
            dbSet.Attach(entity);
            _unitOfWork.Db.Entry(entity).State = EntityState.Modified;
            return this._unitOfWork.Db.SaveChangesAsync();
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
            IEnumerable<T> entities = this.Get(filter);
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

        public virtual Task DeleteAsync(IEnumerable<T> entities)
        {
            foreach (T entity in entities)
            {
                if (_unitOfWork.Db.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }
                dbSet.Remove(entity);
            }
            return this._unitOfWork.Db.SaveChangesAsync();
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
