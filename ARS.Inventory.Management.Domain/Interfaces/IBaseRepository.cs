using System.Linq.Expressions;

namespace ARS.Inventory.Management.Domain.Interfaces
{
    public interface IBaseRespository<T>
    {
        T GetById(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAllPaginated(int page, int pageSize);
        IEnumerable<T> Get(Expression<Func<T, bool>> filter);
        void Insert(T entity);
        Task InsertAsync(T entity);
        void Update(T entity);
        Task UpdateAsync(T entity);
        void UpdateAll(IList<T> entities);
        Task InsertAllAsync(IList<T> entities);
        void Delete(Expression<Func<T, bool>> filter);
        Task DeleteAsync(IEnumerable<T> entities);
        bool Exist(Expression<Func<T, bool>> filter);

    }
}
