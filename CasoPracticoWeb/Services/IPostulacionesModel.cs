using CasoPracticoWeb.Entities;
using InfoBretesWeb.DTO;
using static CasoPracticoWeb.Entities.PostulacionesEnt;
using static CasoPracticoWeb.Entities.PostulacionDTO;


namespace CasoPracticoWeb.Services
{
    public interface IPostulacionesModel {
        PostulacionesRespuesta? ConsultarUnaPostulacion(int idPuesto);
        PostulacionesRespuesta? CrearUnaPostulacion(PostulacionesDTO ent);
        PostulacionesRespuesta? ConsultarPostulacionPorId(int idPostulacion);
        PostulacionDTORespuesta? ConsultarPostulacionPorEmpleado(int idEmpleado);
        PostulacionesRespuesta? ActualizarunaPostulacion(int idPostulacion);
        PostulacionesRespuesta? ActualizarPostulacion(PostulacionesEnt Entidad);
        PostulacionesRespuesta? EliminarPostulacion(int idPostulacion);
    }
}
