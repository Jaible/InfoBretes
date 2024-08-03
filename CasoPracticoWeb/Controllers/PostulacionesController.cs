using Microsoft.AspNetCore.Mvc;
using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Models;
using CasoPracticoWeb.Services;

namespace CasoPracticoWeb.Controllers
{
    public class PostulacionesController(IPostulacionesModel _PostulacionesModel) : Controller
    {
        //Abre la vista:
        [HttpGet]
        public IActionResult ConsultarUnaPostulacion()
        {
            return View();
        }


        [HttpGet]
        public IActionResult ConsultarUnaPostulacion(long IdPuesto)
        {
            var respuestaModelo = _PostulacionesModel.ConsultarUnaPostulacion(IdPuesto);

            if (respuestaModelo?.Codigo == "1")
                return View(respuestaModelo?.Datos);
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View(new List<PostulacionesEnt>());
            }
        }

        

    }
}
