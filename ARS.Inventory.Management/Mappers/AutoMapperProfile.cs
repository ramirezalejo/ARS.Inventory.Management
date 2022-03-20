using ARS.Inventory.Management.Domain.Models;
using ARS.Inventory.Management.Models;
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
            CreateMap<Product, ProductViewModel>();
            CreateMap<Purchase, PurchaseViewModel>();


            CreateMap<CategoryViewModel, Category>();
            CreateMap<OrderViewModel, Order>();
            CreateMap<SupplierViewModel, Supplier>();
            CreateMap<ProductViewModel, Product>();
            CreateMap<PurchaseViewModel, Purchase>();
        }
    }
}
