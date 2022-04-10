using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Web.Models;
using AutoMapper;

namespace ARS.Inventory.Management.Web.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<Supplier, SupplierViewModel>();
            CreateMap<Product, ProductsViewModel>();
            CreateMap<Purchase, PurchaseViewModel>();
            CreateMap<Customer, CustomerViewModel>();

            CreateMap<CategoryViewModel, Category>();
            CreateMap<OrderViewModel, Order>();
            CreateMap<SupplierViewModel, Supplier>();
            CreateMap<ProductsViewModel, Product>();
            CreateMap<PurchaseViewModel, Purchase>();
            CreateMap<CustomerViewModel, Customer>();
        }
    }
}
