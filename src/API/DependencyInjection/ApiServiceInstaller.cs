using API.Journeys.CreateJourney;
using API.Journeys.GetJourneyById;
using API.Journeys.GetJourneys;
using API.Journeys.UpdateJourney;
using API.Orders.CreateOrder;
using API.Orders.GetOrderByUser;

namespace API.DependencyInjection;

public static class ApiServiceInstaller
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services
            .AddScoped<CreateOrderPresenter>()
            .AddScoped<GetOrdersByUserPresenter>();

        services.
            AddScoped<CreateJourneyPresenter>()
            .AddScoped<GetJourneyByIdPresenter>()
            .AddScoped<GetJourneysPresenter>()
            .AddScoped<UpdateJourneyPresenter>();

        return services;
    }
}
