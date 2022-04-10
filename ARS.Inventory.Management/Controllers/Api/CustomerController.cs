using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ARS.Inventory.Management.Web.Controllers.Api
{
    [Authorize]
    [Route("api/Customer")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetByUserId()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok(_mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(_customerService.GetByUserId(user)));
        }

        [Route("api/Customer/GetCustomers")]
        [HttpGet]
        public IActionResult GetAllCustomers(int page = 1, int pageSize = 100)
        {
            return Ok(_mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(_customerService.GetAllPaginated(page, pageSize)));
        }

        [Route("api/Customer/FindCustomersByName")]
        [HttpGet]
        public IActionResult GetAllCustomers(string name)
        {
            return Ok(_mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerViewModel>>(_customerService.GetCustomersByName(name)));
        }

        [Route("api/Customer/{id}")]
        [HttpGet]
        public IActionResult GetAllCustomers([FromRoute]int id)
        {
            return Ok(_mapper.Map<Customer, CustomerViewModel>(_customerService.GetById(id)));
        }

        [HttpPost]
        public IActionResult Insert(CustomerViewModel model)
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            model.CreatedBy = user;
            ModelState.Remove("CreatedBy");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customer = _mapper.Map<Customer>(model);
            _customerService.Insert(customer);
            return Ok(customer);
           
        }

        [HttpPut]
        public IActionResult Update(CustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customer = _mapper.Map<Customer>(model);
            _customerService.Insert(customer);
            return Ok(customer);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _customerService.Delete(id);
            return Ok("Deleted Successfully !");
        }
    }
}
