using ARS.Inventory.Management.Domain.Interfaces;
using ARS.Inventory.Management.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ARS.Inventory.Management.Web.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ListCustomer(int page = 1, int pageSize = 10)
        {
            return View(_mapper.Map<IEnumerable<CustomerViewModel>>(_customerService.GetAllPaginated(page, pageSize)));
        }

        public ActionResult AddCustomer()
        {
            return View();
        }


        public ActionResult EditCustomer(int id)
        {
            if (id > 0)
            {
                var customer = _customerService.GetById(id);

                if (customer != null)
                {
                    return View("AddCustomer", _mapper.Map<CustomerViewModel>(customer));
                }
            }

            return View("AddPurchase");
        }
    }
}
