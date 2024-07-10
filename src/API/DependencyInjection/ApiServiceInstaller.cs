using API.Journeys.CreateJourney;
using API.Journeys.GetJourneyById;
using API.Journeys.GetJourneys;
using API.Journeys.UpdateJourney;
using API.Orders.BeginOrderPayment;
using API.Orders.CompleteOrderPayment;
using API.Orders.CreateOrder;
using API.Orders.GetOrderById;
using API.Orders.GetOrderByUser;
using API.Orders.UpdateOrder;

namespace API.DependencyInjection;

public static class ApiServiceInstaller
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services
            .AddScoped<CreateOrderPresenter>()
            .AddScoped<GetOrdersByUserPresenter>()
            .AddScoped<GetOrderByIdPresenter>()
            .AddScoped<UpdateOrderPresenter>()
            .AddScoped<BeginOrderPaymentPresenter>()
            .AddScoped<CompleteOrderPaymentPresenter>();

        services.
            AddScoped<CreateJourneyPresenter>()
            .AddScoped<GetJourneyByIdPresenter>()
            .AddScoped<GetJourneysPresenter>()
            .AddScoped<UpdateJourneyPresenter>();


        return services;
    }
}
