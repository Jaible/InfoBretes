using InfoBretesWeb.Filters;

namespace InfoBretesWeb.Architecture;

internal static class LocalConfiguration
{
    internal static void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<CustomAuthorizationFilter>();
    }
}