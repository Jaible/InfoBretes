using CasoPracticoWeb.Entities;
using static CasoPracticoWeb.Entities.ComentarioEnt;

namespace CasoPracticoWeb.Services
{
    public interface IComentarioModel
    {
        ComentarioRespuesta? ConsultarComentario();

        ComentarioRespuesta? RegistrarComentario(ComentarioEnt entidad);
    }
}
