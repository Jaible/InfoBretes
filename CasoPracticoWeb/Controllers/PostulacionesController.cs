﻿using Microsoft.AspNetCore.Mvc;
using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Models;
using CasoPracticoWeb.Services;
using InfoBretesWeb.Filters;

namespace CasoPracticoWeb.Controllers;

[ServiceFilter(typeof(CustomAuthorizationFilter))]
public class PostulacionesController(IPostulacionesModel _PostulacionesModel) : Controller
{

    [HttpGet]
    public IActionResult ConsultarUnaPostulacion(int IdPuesto)
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
