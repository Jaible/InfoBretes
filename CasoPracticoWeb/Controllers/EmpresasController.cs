using Microsoft.AspNetCore.Mvc;
using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Models;
using CasoPracticoWeb.Services;
using InfoBretesWeb.Filters;

namespace CasoPracticoWeb.Controllers;

[ServiceFilter(typeof(CustomAuthorizationFilter))]
public class EmpresasController(IEmpresasModel _EmpresasModel) : Controller
{


    //Abre la vista:
    [HttpGet]
    public IActionResult RegistrarEmpresas()
    {
        return View();
    }

    [HttpPost]
    public IActionResult RegistrarEmpresas(EmpresasEnt entidad)
    {
        var RespuestaApi = _EmpresasModel.RegistrarEmpresas(entidad);

        if (RespuestaApi?.Codigo == "1")
            return RedirectToAction("ConsultarEmpresas", "Empresas");
        else
        {
            return RedirectToAction("ConsultarEmpresas", "Empresas");
        }
    }

    [HttpGet]
    public IActionResult ConsultarEmpresas()
    {
        var respuestaModelo = _EmpresasModel.ConsultarEmpresas();

        if (respuestaModelo?.Codigo == "1")
            return View(respuestaModelo?.Datos);
        else
        {
            ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
            return View(new List<EmpresasEnt>());
        }
    }




}
