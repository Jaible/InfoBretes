using Microsoft.AspNetCore.Mvc;
using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Services;
using InfoBretesWeb.Services;
using System.Security.Claims;
using InfoBretesWeb.Entities;

namespace CasoPracticoWeb.Controllers {
    public class ComentarioController(IComentarioModel _ComentarioModel, IUserModel iUserModel) : Controller
    {


        //Abre la vista:
        [HttpGet]
        public IActionResult RegistrarComentario(int id)
        {
            UserEnt user = new UserEnt { Email = User.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault() };
            var respuesta = iUserModel.Perfil(user);
            ComentarioEnt comentario = new ComentarioEnt {
                idEmpresa = id,
                idUsuario = respuesta?.Dato?.IdUsuario
            };
            return View(comentario);
        }

        [HttpPost]
        public IActionResult RegistrarComentario(ComentarioEnt entidad)
        {
            var respuestaModelo = _ComentarioModel.RegistrarComentario(entidad);

            if (respuestaModelo?.Codigo == "1")
                return RedirectToAction("ConsultarEmpresas", "Empresas");
            else {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return RedirectToAction("RegistrarComentario", "Comentario");
            }
        }

        [HttpGet]
        public IActionResult ConsultarComentario(int idEmpresa)
        {
            var respuestaModelo = _ComentarioModel.ConsultarComentario(idEmpresa);

            if (respuestaModelo?.Codigo == "1")
                return View(respuestaModelo?.Datos);
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View(new List<ComentarioDTO>());
            }
        }


    }
}
