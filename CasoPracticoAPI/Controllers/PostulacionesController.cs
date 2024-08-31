﻿using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CasoPracticoAPI.Entities;
using System.Data;
using System.Data.SqlClient;
using static InfoBretesAPI.Entities.PostulacionDTO;
using static CasoPracticoAPI.Entities.PostulacionesEnt;
using InfoBretesAPI.DTO;
using InfoBretesAPI.Services;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using InfoBretesAPI.Entities;

namespace InfoBretesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostulacionesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly EmailService _emailService;

        public PostulacionesController(IConfiguration configuration, EmailService emailService)
        {
            _configuration = configuration;
            _emailService = emailService;
        }


        [AllowAnonymous]
        [Route("ConsultarUnaPostulacion")]
        [HttpGet]
        public IActionResult ConsultarUnaPostulacion(int idPuesto)
        {
            PostulacionesRespuesta PostulacionesRespuesta = new PostulacionesRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Query<PostulacionesEnt>("ObtenerSolicitudPorId",
                        new { idPuesto },
                        commandType: CommandType.StoredProcedure).ToList();

                    if (result == null)
                    {
                        PostulacionesRespuesta.Codigo = "-1";
                        PostulacionesRespuesta.Mensaje = "No hay Postulaciones registrados.";
                    }
                    else
                    {
                        PostulacionesRespuesta.Datos = result;
                        PostulacionesRespuesta.Codigo = "1";
                        PostulacionesRespuesta.Mensaje = "Postulaciones consultado con éxito.";
                    }

                    return Ok(PostulacionesRespuesta);
                }
            }
            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al consultar las Postulaciones en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al consultar las Postulaciones.", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("CrearPostulacion")]
        public async Task<IActionResult> CrearUnaPostulacion(PostulacionesDTO ent)
        {
            PostulacionesRespuesta resp = new PostulacionesRespuesta();
            using var context = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            var result = context.Execute("RegistrarPostulacion", new { ent.idEmpleado, ent.idPuesto },
                         commandType: CommandType.StoredProcedure);

            if (result > 0)
            {
                var user = context.Query<EmpleadoUsuarioDTO>("ObtenerUsuarioPorEmpleadoId",
                             new { ent.idEmpleado },
                             commandType: CommandType.StoredProcedure).FirstOrDefault();
                var puesto = context.Query<PuestosTrabajoEnt>("ObtenerPuestoPorId",
                             new { ent.idPuesto },
                             commandType: CommandType.StoredProcedure).FirstOrDefault();
                var empresa = context.Query<EmpresasEnt>("ObtenerEmpresaPorId",
                              new { puesto.idEmpresa },
                              commandType: CommandType.StoredProcedure).FirstOrDefault();

                await _emailService.SendEmail(user.Email, user.Nombre, puesto.titulo, empresa.correo);
                resp.Codigo = "1";
                resp.Mensaje = "La postulacion ha sido un exito";
            } else
            {
                resp.Codigo = "0";
                resp.Mensaje = "Hubo un error en la postulacion";
            }
            return Ok(resp);
        }

        // Consultar una Postulación existente po id
        [AllowAnonymous]
        [Route("ConsultarPostulacionPorId")]
        [HttpGet]
        public IActionResult ConsultarPostulacionPorId(int idPostulacion) {
            PostulacionesRespuesta PostulacionesRespuesta = new PostulacionesRespuesta();
            try {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                    var result = db.Query<PostulacionesEnt>("ConsultarPostulacionPorId",
                        new { idPostulacion },
                        commandType: CommandType.StoredProcedure).ToList();

                    if (result == null) {
                        PostulacionesRespuesta.Codigo = "-1";
                        PostulacionesRespuesta.Mensaje = "No hay Postulaciones registrados.";
                    } else {
                        PostulacionesRespuesta.Datos = result;
                        PostulacionesRespuesta.Codigo = "1";
                        PostulacionesRespuesta.Mensaje = "Postulacion consultada con éxito.";
                    }

                    return Ok(PostulacionesRespuesta);
                }
            } catch (SqlException ex) {

                return StatusCode(500, new { message = "Error al consultar la Postulacion en la base de datos.", error = ex.Message });
            } catch (Exception ex) {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al consultar la Postulacion.", error = ex.Message });
            }
        }




        // Actualizar una Postulación existente
        [AllowAnonymous]
        [Route("ActualizarunaPostulacion")]
        [HttpGet]
        public IActionResult ActualizarunaPostulacion(long idEmpleado) {
            PostulacionesRespuesta PostulacionesRespuesta = new PostulacionesRespuesta();
            try {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                    var result = db.Query<PostulacionesEnt>("ActualizarUnaPostulacion",
                        new { idEmpleado },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (result == null) {

                        PostulacionesRespuesta.Codigo = "-1";
                        PostulacionesRespuesta.Mensaje = "No se pudo actualizar la postulación.";
                    } else {
                        PostulacionesRespuesta.Dato = result;
                        PostulacionesRespuesta.Codigo = "1";
                        PostulacionesRespuesta.Mensaje = "Postulación actualizada con éxito";
                    }

                    return Ok(PostulacionesRespuesta);
                }
            } catch (SqlException ex) {
                return StatusCode(500, new { message = "Error al actualizar la Postulación en la base de datos.", error = ex.Message });
            } catch (Exception ex) {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al actualizar la Postulación.", error = ex.Message });
            }
        }
        [AllowAnonymous]
        [Route("ActualizarPostulacion")]
        [HttpPut]
        public IActionResult ActualizarPostulacion(PostulacionesEnt Postulacion) {
            PostulacionesRespuesta PostulacionesRespuesta = new PostulacionesRespuesta();
            try {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                    var result = db.Execute("ActualizarPostulacion",
                        new {
                            Postulacion.idEmpleado,
                            Postulacion.idPuesto,
                            Postulacion.fechaPostulacion,
                            Postulacion.estadoPostulacion
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0) {
                        PostulacionesRespuesta.Codigo = "-1";
                        PostulacionesRespuesta.Mensaje = "No se pudo actualizar la postulación";
                        return BadRequest(PostulacionesRespuesta);
                    } else {
                        PostulacionesRespuesta.Codigo = "1";
                        PostulacionesRespuesta.Mensaje = "Postulación actualizada con éxito";
                        return Ok(PostulacionesRespuesta);
                    }
                }
            } catch (SqlException ex) {

                return StatusCode(500, new { message = "Error al actualizar la postulacion en la base de datos.", error = ex.Message });
            } catch (Exception ex) {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al actualizar la postulacion.", error = ex.Message });
            }
        }



        // Eliminar una Postulación
        [AllowAnonymous]
        [Route("EliminarPostulacion")]
        [HttpDelete]
        public IActionResult EliminarPostulacion(long idPostulacion) {
            PostulacionesRespuesta PostulacionesRespuesta = new PostulacionesRespuesta();
            try {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                    var result = db.Execute("EliminarPostulacion",
                        new {
                            idPostulacion
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0) {
                        PostulacionesRespuesta.Codigo = "-1";
                        PostulacionesRespuesta.Mensaje = "No se pudo eliminar la Postulación ";
                        return BadRequest(PostulacionesRespuesta);
                    } else {
                        PostulacionesRespuesta.Codigo = "1";
                        PostulacionesRespuesta.Mensaje = "Postulación eliminada con exito.";
                        return Ok(PostulacionesRespuesta);
                    }
                }
            } catch (SqlException ex) {

                return StatusCode(500, new { message = "Error al eliminar la postulacion en la base de datos.", error = ex.Message });
            } catch (Exception ex) {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al eliminar la postulacion.", error = ex.Message });
            }

        }

        // Consultar una Postulación existente por empleado
        [AllowAnonymous]
        [Route("ConsultarPostulacionPorEmpleado")]
        [HttpGet]
        public IActionResult ConsultarPostulacionPorEmpleado(int idEmpleado) {
            PostulacionDTORespuesta PostulacionesRespuesta = new PostulacionDTORespuesta();
            try {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"))) {
                    var result = db.Query<PostulacionDTO>("ConsultarPostulacionPorEmpleado",
                        new { idEmpleado },
                        commandType: CommandType.StoredProcedure).ToList();

                    if (result == null) {
                        PostulacionesRespuesta.Codigo = "-1";
                        PostulacionesRespuesta.Mensaje = "No hay Postulaciones registrados.";
                    } else {
                        PostulacionesRespuesta.Datos = result;
                        PostulacionesRespuesta.Codigo = "1";
                        PostulacionesRespuesta.Mensaje = "Postulacion consultada con éxito.";
                    }

                    return Ok(PostulacionesRespuesta);
                }
            } catch (SqlException ex) {

                return StatusCode(500, new { message = "Error al consultar la Postulacion en la base de datos.", error = ex.Message });
            } catch (Exception ex) {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al consultar la Postulacion.", error = ex.Message });
            }
        }
    }
}


