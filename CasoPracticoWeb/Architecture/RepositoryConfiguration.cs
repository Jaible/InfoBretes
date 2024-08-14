using CasoPracticoWeb.Models;
using CasoPracticoWeb.Services;
using InfoBretesWeb.Models;
using InfoBretesWeb.Services;

namespace InfoBretesWeb.Architecture;

internal static class RepositoryConfiguration
{
    internal static void Register(IServiceCollection serviceCollection)
    {

        serviceCollection.AddSingleton<IPostulacionesModel, PostulacionesModel>();
        serviceCollection.AddSingleton<IPuestosTrabajoModel, PuestosTrabajoModel>();
        serviceCollection.AddSingleton<IEmpresasModel, EmpresasModel>();
        serviceCollection.AddSingleton<IUserModel, UserModel>();
    }
}