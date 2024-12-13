using BasiliskBusiness;
using BasiliskBusiness.Interface;
using BasiliskBusiness.Interfaces;
using BasiliskBusiness.Repositories;
using BasiliskWeb.BackendGateway;
using BasiliskWeb.Services;

namespace BasiliskWeb;

public static class ConfigureBusinessService
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services) {
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<SupplierService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<CategoryService>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ProductService>();
        services.AddScoped<IRegionRepository, RegionRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<CustomerService>();
        services.AddScoped<IDeliveryRepository, DeliveryRepository>();
        services.AddScoped<DeliveryService>();
        services.AddScoped<ISalesmanRepository, SalesmanRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<OrderService>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<AccountService>();
        services.AddScoped<SalesmanGatewayService>();
        return services;
    }
}