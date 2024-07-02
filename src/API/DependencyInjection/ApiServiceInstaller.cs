using API.Orders.CreateOrder;
using API.Orders.GetOrderByUser;

namespace API.DependencyInjection;

public static class ApiServiceInstaller
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        return services
            .AddScoped<CreateOrderPresenter>()
            .AddScoped<GetOrdersByUserPresenter>();
    }
}
