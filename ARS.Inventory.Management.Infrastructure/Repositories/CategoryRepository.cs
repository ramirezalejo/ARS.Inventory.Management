using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Inventory.Management.Infrastructure.Repository
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        
    }
}
