using CasoPracticoWeb.Entities;
using static CasoPracticoWeb.Entities.PostulacionesEnt;


namespace CasoPracticoWeb.Services
{
    public interface IPostulacionesModel
    {
        PostulacionesRespuesta? ConsultarUnaPostulacion(long IdPuesto);
    }
}
