using InfoBretesWeb.Entities;
using InfoBretesWeb.Models;
using InfoBretesWeb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InfoBretesWeb.Controllers
{
    public class EmpleadosController(IEmpleadosModel iEmpleadosModel, IUserModel iUserModel) : Controller
    {
        [HttpGet("CreaEmpleado")]
        public IActionResult CreaEmpleado()
        {
            return View();
        }

        [HttpPost("CreaEmpleado")]
        public IActionResult CreaEmpleado(EmpleadosEnt ent)
        {
            UserEnt user = new UserEnt { Email = User.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault() };
            var respuesta = iUserModel.Perfil(user);
            ent.idUsuario = (int)respuesta.Dato.IdUsuario;
            ent.fotoPerfil = "";
            var resp = iEmpleadosModel.CrearEmpleado(ent);

            if (resp?.Codigo == "1")
            {
                return RedirectToAction("Profile", "Home");
            } else
            {
                return View(ent);
            }
        }
    }
}
