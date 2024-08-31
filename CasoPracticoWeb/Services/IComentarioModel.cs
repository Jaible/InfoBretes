using CasoPracticoWeb.Entities;
using static CasoPracticoWeb.Entities.ComentarioEnt;
using static CasoPracticoWeb.Entities.ComentarioDTO;

namespace CasoPracticoWeb.Services
{
    public interface IComentarioModel
    {
        ComentarioDTORespuesta? ConsultarComentario(int idEmpresa);

        ComentarioRespuesta? RegistrarComentario(ComentarioEnt entidad);
    }
}
