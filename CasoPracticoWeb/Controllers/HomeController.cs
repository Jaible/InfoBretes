using CasoPracticoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using InfoBretesAPI.Models;


namespace InfoBretesWeb.Controllers
{
    public class PostulacionesController : Controller
    {
        private readonly IConfiguration _configuration;

        public PostulacionesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Consultar una Postulaci�n
        public IActionResult ConsultarUnaPostulacion(int idPuesto)
        {
            PostulacionesRespuesta PostulacionesRespuesta = new PostulacionesRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Query<PostulacionEnt>("ObtenerSolicitudPorId",
                        new { idPuesto },
                        commandType: CommandType.StoredProcedure).ToList();

                    if (result == null || result.Count == 0)
                    {
                        ViewBag.Mensaje = "No hay postulaciones registradas.";
                        return View("Error");
                    }

                    PostulacionesRespuesta.Datos = result;
                    PostulacionesRespuesta.Codigo = "1";
                    PostulacionesRespuesta.Mensaje = "Postulaci�n consultada con �xito.";

                    return View(PostulacionesRespuesta.Datos);
                }
            }
            catch (SqlException ex)
            {
                ViewBag.Mensaje = "Error al consultar las postulaciones en la base de datos: " + ex.Message;
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Ocurri� un error inesperado al consultar las postulaciones: " + ex.Message;
                return View("Error");
            }
        }

        // Crear una nueva Postulaci�n
        [HttpPost]
        public IActionResult CrearPostulacion(PostulacionEnt nuevaPostulacion)
        {
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("CrearPostulacion",
                        new
                        {
                            nuevaPostulacion.idEmpleado,
                            nuevaPostulacion.idPuesto,
                            nuevaPostulacion.FechaPostulacion,
                            nuevaPostulacion.estadoPostulacion
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result > 0)
                    {
                        TempData["Mensaje"] = "Postulaci�n creada con �xito.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Mensaje"] = "No se pudo crear la postulaci�n.";
                        return RedirectToAction("Crear");
                    }
                }
            }
            catch (SqlException ex)
            {
                ViewBag.Mensaje = "Error al crear la postulaci�n en la base de datos: " + ex.Message;
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Ocurri� un error inesperado al crear la postulaci�n: " + ex.Message;
                return View("Error");
            }
        }

        // Actualizar una Postulaci�n existente
        [HttpPost]
        public IActionResult ActualizarPostulacion(PostulacionEnt postulacionActualizada)
        {
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("ActualizarPostulacion",
                        new
                        {
                            postulacionActualizada.idPostulacion,
                            postulacionActualizada.idEmpleado,
                            postulacionActualizada.idPuesto,
                            postulacionActualizada.FechaPostulacion,
                            postulacionActualizada.estadoPostulacion
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result > 0)
                    {
                        TempData["Mensaje"] = "Postulaci�n actualizada con �xito.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Mensaje"] = "No se pudo actualizar la postulaci�n.";
                        return RedirectToAction("Editar", new { id = postulacionActualizada.idPostulacion });
                    }
                }
            }
            catch (SqlException ex)
            {
                ViewBag.Mensaje = "Error al actualizar la postulaci�n en la base de datos: " + ex.Message;
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Ocurri� un error inesperado al actualizar la postulaci�n: " + ex.Message;
                return View("Error");
            }
        }

        // Eliminar una Postulaci�n
        [HttpPost]
        public IActionResult EliminarPostulacion(int idPostulacion)
        {
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("EliminarPostulacion",
                        new { idPostulacion },
                        commandType: CommandType.StoredProcedure);

                    if (result > 0)
                    {
                        TempData["Mensaje"] = "Postulaci�n eliminada con �xito.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Mensaje"] = "No se pudo eliminar la postulaci�n.";
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (SqlException ex)
            {
                ViewBag.Mensaje = "Error al eliminar la postulaci�n en la base de datos: " + ex.Message;
                return View("Error");
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Ocurri� un error inesperado al eliminar la postulaci�n: " + ex.Message;
                return View("Error");
            }
        }
    }
}
