using CasoPracticoWeb.Entities;
using static CasoPracticoWeb.Entities.PostulacionEnt;


namespace CasoPracticoWeb.Services
{
    public interface IPostulacionesModel
    {
        PostulacionesRespuesta? ConsultarPostulacionPorId(int idPostulacion);
        PostulacionesRespuesta? ConsultarPostulaciones();
        PostulacionesRespuesta? CrearPostulacion(PostulacionEnt Entidad);
        PostulacionesRespuesta? ActualizarunaPostulacion(long idPostulacion);
        PostulacionesRespuesta? ActualizarPostulacion(PostulacionEnt Entidad);
        PostulacionesRespuesta? EliminarPostulacion(int idPostulacion);


    }
}
