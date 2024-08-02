using InfoBretesWeb.Entities;
using static InfoBretesWeb.Entities.UserEnt;

namespace InfoBretesWeb.Models
{
    public interface IUserModel
    {
        public UserRespuesta RegistrarUsuario(UserEnt user);
        public UserRespuesta IniciarSesion(Login user);
        public UserRespuesta Perfil(UserEnt user);
        public UserRespuesta ModificaPerfil(UserEnt user);
        public UserRespuesta ActualizarUsuario(UserEnt user);
    }
}
