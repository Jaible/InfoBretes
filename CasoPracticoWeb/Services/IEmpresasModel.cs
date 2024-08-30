using CasoPracticoWeb.Entities;
using static CasoPracticoWeb.Entities.EmpresasEnt;

namespace CasoPracticoWeb.Services
{
    public interface IEmpresasModel
    {
        EmpresasRespuesta? ConsultarEmpresas();

        EmpresasRespuesta? RegistrarEmpresas(EmpresasEnt entidad);
    }
}
