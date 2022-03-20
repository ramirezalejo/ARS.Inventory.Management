using System.Linq.Expressions;

namespace ARS.Inventory.Management.Domain.Interfaces
{
    public interface IBaseRespository<T>
    {
        T GetById(Expression<Func<T, bool>> filter);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);
        T Insert(T entity);
        void Update(T entity);
        void UpdateAll(IList<T> entities);
        void Delete(Expression<Func<T, bool>> filter);
        bool Exist(Expression<Func<T, bool>> filter);

    }
}
