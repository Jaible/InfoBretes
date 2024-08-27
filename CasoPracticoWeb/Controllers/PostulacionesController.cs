using Microsoft.AspNetCore.Mvc;
using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Models;
using CasoPracticoWeb.Services;




namespace CasoPracticoWeb.Controllers
{
    public class PostulacionesController (IPostulacionesModel _PostulacionesModel) : Controller
    {


        //Abre la vista:
        [HttpGet]
        public IActionResult CrearPostulacion()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearPostulacion(PostulacionEnt entidad)
        {
            var RespuestaApi = _PostulacionesModel.CrearPostulacion(entidad);

            if (RespuestaApi?.Codigo == "1")
                return RedirectToAction("ConsultarPostulaciones", "Postulacion");
            else
            {
                return RedirectToAction("ConsultarPostulaciones", "Postulacion");
            }
        }

        [HttpGet]
        public IActionResult ConsultarPostulaciones()
        {
            var respuestaModelo = _PostulacionesModel.ConsultarPostulaciones();

            if (respuestaModelo?.Codigo == "1")
                return View(respuestaModelo?.Datos);
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View(new List<PostulacionEnt>());
            }
        }

        [HttpGet]
        public IActionResult ConsultarPostulacionPorId(long idPostulacion)
        {
            var respuestaModelo = _PostulacionesModel.ConsultarPostulacionPorId(idPostulacion);

            if (respuestaModelo?.Codigo == "1")
                return View(respuestaModelo?.Datos);
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View(new List<PostulacionEnt>());
            }
        }

        [HttpGet]
        public IActionResult ActualizarunaPostulacion(long idPostulacion)
        {
            var respuestaModelo = _PostulacionesModel.ActualizarunaPostulacion(idPostulacion);
            return View(respuestaModelo?.Dato);
        }


        [HttpPost]
        public IActionResult ActualizarPostulacion(PostulacionEnt entidad)
        {
            var respuestaModelo = _PostulacionesModel.ActualizarPostulacion(entidad);

            if (respuestaModelo?.Codigo == "1")
                return RedirectToAction("ConsultarPostulaciones", "Postulacion");
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return View();
            }
        }

        [HttpPost]
        public IActionResult EliminarPostulacion(PostulacionEnt entidad)
        {
            var respuestaModelo = _PostulacionesModel.EliminarPostulacion(entidad.idPostulacion);

            if (respuestaModelo?.Codigo == "1")
                return RedirectToAction("ConsultarPostulaciones", "Postulacion");
            else
            {
                ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
                return RedirectToAction("ConsultarPostulaciones", "Postulacion");
            }
        }



    }
}