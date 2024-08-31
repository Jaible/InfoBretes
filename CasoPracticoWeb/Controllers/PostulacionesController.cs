using Microsoft.AspNetCore.Mvc;
using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Models;
using CasoPracticoWeb.Services;
using InfoBretesWeb.Filters;
using System.Security.Claims;
using InfoBretesWeb.Entities;
using InfoBretesWeb.Services;
using InfoBretesWeb.DTO;

namespace CasoPracticoWeb.Controllers;

[ServiceFilter(typeof(CustomAuthorizationFilter))]
public class PostulacionesController(IPostulacionesModel _PostulacionesModel, IUserModel iUserModel, IEmpleadosModel iEmpleadosModel) : Controller
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

    [HttpGet]
    public IActionResult CrearUnaPostulacion(int id)
    {
        UserEnt user = new UserEnt { Email = User.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault() };
        var respuesta = iUserModel.Perfil(user);
        var emp = iEmpleadosModel.ConsultarEmpleado((int)respuesta.Dato.IdUsuario);

        if(emp?.Codigo == "1")
        {
            PostulacionesDTO post = new PostulacionesDTO { idPuesto = id, idEmpleado = emp.Dato.idEmpleado };
            var resp = _PostulacionesModel.CrearUnaPostulacion(post);

            if (respuesta?.Codigo == "1")
            {
                return RedirectToAction("ConsultarPuestosTrabajo", "PuestosTrabajo");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        } else
        {
            return RedirectToAction("CreaEmpleado", "Empleados");
        }
    }

    [HttpGet]
    public IActionResult ConsultarPostulacionPorEmpleado() {
        UserEnt user = new UserEnt { Email = User.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault() };
        var respuesta = iUserModel.Perfil(user);
        var empleado = iEmpleadosModel.ConsultarEmpleado((int)respuesta?.Dato?.IdUsuario);
        if(empleado?.Dato?.idEmpleado == null) {
            return RedirectToAction("CreaEmpleado", "Empleados");
        }
        var respuestaModelo = _PostulacionesModel.ConsultarPostulacionPorEmpleado((int)empleado?.Dato?.idEmpleado);

        if (respuestaModelo?.Codigo == "1")
            return View(respuestaModelo?.Datos);
        else {
            ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
            return View(new List<PostulacionesEnt>());
        }
    }


    [HttpGet]
    public IActionResult EliminarPostulacion(int id) {
        var respuestaModelo = _PostulacionesModel.EliminarPostulacion(id);

        if (respuestaModelo?.Codigo == "1")
            return RedirectToAction("ConsultarPostulacionPorEmpleado", "Postulaciones");
        else {
            ViewBag.MsjPantalla = respuestaModelo?.Mensaje;
            return RedirectToAction("ConsultarPostulacionPorEmpleado", "Postulaciones");
        }
    }
}
