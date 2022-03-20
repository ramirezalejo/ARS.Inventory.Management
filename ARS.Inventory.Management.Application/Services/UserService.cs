using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Infrastructure.Interfaces;
using ARS.Inventory.Management.Infrastructure.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARS.Inventory.Management.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserRepository _userRepository;

        public UserService(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
            this._userRepository = new UserRepository(unitOfWork);
        }

        public void Delete(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            var result = _userRepository.GetAll();
            return result;
        }

        public ApplicationUser GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Insert(ApplicationUser category)
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser category)
        {
            throw new NotImplementedException();
        }
    }
}
