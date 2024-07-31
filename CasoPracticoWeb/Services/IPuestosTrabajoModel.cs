using CasoPracticoWeb.Entities;
using static CasoPracticoWeb.Entities.PuestosTrabajoEnt;

namespace CasoPracticoWeb.Services
{
    public interface IPuestosTrabajoModel
    {
        PuestosTrabajoRespuesta? ConsultarPuestosTrabajo();

        PuestosTrabajoRespuesta? ConsultarUnPuestoTrabajo(long idPuesto);
        PuestosTrabajoRespuesta? RegistrarPuestosTrabajo(PuestosTrabajoEnt entidad);
        PuestosTrabajoRespuesta? ActualizarPuestosTrabajo(PuestosTrabajoEnt entidad);

        PuestosTrabajoRespuesta? EliminarPuestosTrabajo(long idPuesto);
    }
}
