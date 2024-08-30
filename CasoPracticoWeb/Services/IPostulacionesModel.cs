using CasoPracticoWeb.Entities;
using InfoBretesWeb.DTO;
using static CasoPracticoWeb.Entities.PostulacionesEnt;



namespace CasoPracticoWeb.Services
{
    public interface IPostulacionesModel
    {
        PostulacionesRespuesta? ConsultarUnaPostulacion(int idPuesto);
        PostulacionesRespuesta? CrearUnaPostulacion(PostulacionesDTO ent);

    }
}
