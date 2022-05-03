using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repository;

namespace ARS.Inventory.Management.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CategoryRepository categoryRepository;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.categoryRepository = new CategoryRepository(_unitOfWork);
        }

        public IEnumerable<Category> GetAll()
        {
            var result = categoryRepository.GetAll();
            return result;
        }

        public Task<IEnumerable<Category>> GetAllAsync()
        {
            return categoryRepository.GetAllAsync();
        }

        public Category GetById(int categoryId)
        {
            Category result = null;

            if (categoryId != null)
                result = categoryRepository.GetById(c => c.Id == categoryId);

            return result;
        }

        public void Insert(Category category)
        {
            if (category != null)
                categoryRepository.Insert(category);
        }

        public async Task InsertAsync(Category category)
        {
            if (category != null)
                await categoryRepository.InsertAsync(category);
        }

        public void Update(Category category)
        {
            if (category != null)
                categoryRepository.Update(category);
        }

        public void Delete(int categoryId)
        {
            if (categoryId != null)
                categoryRepository.Delete(c => c.Id == categoryId);
        }
    }
}
