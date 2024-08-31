using InfoBretesWeb.Entities;
using static InfoBretesWeb.Entities.EmpleadosEnt;

namespace InfoBretesWeb.Services
{
    public interface IEmpleadosModel
    {
        EmpleadosRespuesta? ConsultarEmpleado(int id);
        EmpleadosRespuesta? CrearEmpleado(EmpleadosEnt ent);
    }
}
