using Microsoft.AspNetCore.Mvc;
using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Models;
using CasoPracticoWeb.Services;
using CasoPracticoWeb.Entities;

namespace CasoPracticoWeb.Controllers
{
    public class ComentarioController(IComentarioModel _ComentarioModel) : Controller
    {


        //Abre la vista:
        [HttpGet]
        public IActionResult RegistrarComentario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarComentario(ComentarioEnt entidad)
        {
            var respuestaModelo = _ComentarioModel.ConsultarComentario();

            if (respuestaModelo?.Codigo == "1")
                return View(respuestaModelo?.Datos);
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View(new List<EmpresasEnt>());
            }
        }

        [HttpGet]
        public IActionResult ConsultarComentario()
        {
            var respuestaModelo = _ComentarioModel.ConsultarComentario();

            if (respuestaModelo?.Codigo == "1")
                return View(respuestaModelo?.Datos);
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View(new List<EmpresasEnt>());
            }
        }


    }
}
