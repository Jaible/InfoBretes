using Microsoft.AspNetCore.Mvc;
using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Models;
using CasoPracticoWeb.Services;
using InfoBretesWeb.Filters;

namespace CasoPracticoWeb.Controllers;

[ServiceFilter(typeof(CustomAuthorizationFilter))]
public class PuestosTrabajoController(IPuestosTrabajoModel _PuestosTrabajoModel) : Controller
{


    //Abre la vista:
    [HttpGet]
    public IActionResult RegistrarPuestosTrabajo()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RegistrarPuestosTrabajo(PuestosTrabajoEnt entidad)
    {
        var RespuestaApi = _PuestosTrabajoModel.RegistrarPuestosTrabajo(entidad);

        if (RespuestaApi?.Codigo == "1")
            return RedirectToAction("ConsultarPuestosTrabajo", "PuestosTrabajo");
        else
        {
            return RedirectToAction("ConsultarPuestosTrabajo", "PuestosTrabajo");
        }
    }

    [HttpGet]
    public IActionResult ConsultarPuestosTrabajo()
    {
        var respuestaModelo = _PuestosTrabajoModel.ConsultarPuestosTrabajo();

        if (respuestaModelo?.Codigo == "1")
            return View(respuestaModelo?.Datos);
        else
        {
            ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
            return View(new List<PuestosTrabajoEnt>());
        }
    }

    [HttpGet]
    public IActionResult ConsultarUnPuestoTrabajo(long idPuesto)
    {
        var respuestaModelo = _PuestosTrabajoModel.ConsultarUnPuestoTrabajo(idPuesto);

        if (respuestaModelo?.Codigo == "1")
            return View(respuestaModelo?.Datos);
        else
        {
            ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
            return View(new List<PuestosTrabajoEnt>());
        }
    }

    [HttpGet]
    public IActionResult ActualizarPuestosTrabajo(long idPuesto)
    {
        var respuestaModelo = _PuestosTrabajoModel.ActualizarUnPuestosTrabajo(idPuesto);
        return View(respuestaModelo?.Dato);
    }


    [HttpPost]
    public IActionResult ActualizarPuestosTrabajo(PuestosTrabajoEnt entidad)
    {
        var respuestaModelo = _PuestosTrabajoModel.ActualizarPuestosTrabajo(entidad);

        if (respuestaModelo?.Codigo == "1")
            return RedirectToAction("ConsultarPuestosTrabajo", "PuestosTrabajo");
        else
        {
            ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
            return View();
        }
    }

    [HttpPost]
    public IActionResult EliminarPuestosTrabajo(PuestosTrabajoEnt entidad)
    {
        var respuestaModelo = _PuestosTrabajoModel.EliminarPuestosTrabajo(entidad.idPuesto);

        if (respuestaModelo?.Codigo == "1")
            return RedirectToAction("ConsultarPuestosTrabajo", "PuestosTrabajo");
        else
        {
            ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
            return RedirectToAction("ConsultarPuestosTrabajo", "PuestosTrabajo");
        }
    }



}
