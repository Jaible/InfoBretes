using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CasoPracticoAPI.Entities;
using System.Data;
using System.Data.SqlClient;
using InfoBretesAPI.Models;

namespace InfoBretesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostulacionesController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PostulacionesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        // Consultar Postulaciones
        [Authorize]
        [Route("ConsultarPostulaciones")]
        [HttpGet]
        public IActionResult ConsultarPostulaciones()
        {
            PostulacionesRespuesta PostulacionesRespuesta = new PostulacionesRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Query<PostulacionEnt>("ConsultarPostulaciones", new { }, commandType: CommandType.StoredProcedure).ToList();

                    if (result == null || result.Count == 0)
                    {
                        PostulacionesRespuesta.Codigo = "-1";
                        PostulacionesRespuesta.Mensaje = "No hay Postulaciones registradas.";
                    }
                    else
                    {
                        PostulacionesRespuesta.Datos = result;
                        PostulacionesRespuesta.Codigo = "1";
                        PostulacionesRespuesta.Mensaje = "Postulación consultada con éxito.";
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
        

        // Crear una nueva Postulación
        [AllowAnonymous]
        [Route("CrearPostulacion")]
        [HttpPost]
        public IActionResult CrearPostulacion(PostulacionEnt Postulacion)
        {
            var PostulacionesRespuesta = new PostulacionesRespuesta();
            try
            {

                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var parametros = new
                    {
                        Postulacion.idEmpleado,
                        Postulacion.idPuesto,
                        Postulacion.FechaPostulacion,
                        Postulacion.estadoPostulacion
                    };
                    var result = db.Execute("CrearPostulacion", parametros, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        PostulacionesRespuesta.Codigo = "1";
                        PostulacionesRespuesta.Mensaje = "Postulacion creada";
                        return Ok(PostulacionesRespuesta);
                    }
                    else
                    {
                        PostulacionesRespuesta.Codigo = "-1";
                        PostulacionesRespuesta.Mensaje = "No se pudo hacer la postulacion";
                        return BadRequest(PostulacionesRespuesta);
                    }
                }
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al actualizar la Postulación.", error = ex.Message });
            }
        }
        // Consultar una Postulación existente
        [AllowAnonymous]
        [Route("ConsultarPostulacionPorId")]
        [HttpGet]
        public IActionResult ConsultarPostulacionPorId(int idPostulacion)
        {
            PostulacionesRespuesta PostulacionesRespuesta = new PostulacionesRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Query<PostulacionEnt>("ConsultarPostulacionPorId",
                        new { idPostulacion },
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
                        PostulacionesRespuesta.Mensaje = "Postulacion consultada con éxito.";
                    }

                    return Ok(PostulacionesRespuesta);
                }
            }
            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al consultar la Postulacion en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al consultar la Postulacion.", error = ex.Message });
            }
        }




        // Actualizar una Postulación existente
        [AllowAnonymous]
        [Route("ActualizarunaPostulacion")]
        [HttpGet]
        public IActionResult ActualizarunaPostulacion( long idEmpleado)
        {
            PostulacionesRespuesta PostulacionesRespuesta = new PostulacionesRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Query < PostulacionEnt>("ActualizarUnaPostulacion",
                        new { idEmpleado }, 
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (result == null)
                    {
                        
                        PostulacionesRespuesta.Codigo = "-1";
                        PostulacionesRespuesta.Mensaje = "No se pudo actualizar la postulación.";
                    }
                    else
                    {
                        PostulacionesRespuesta.Dato = result;
                        PostulacionesRespuesta.Codigo = "1";
                        PostulacionesRespuesta.Mensaje = "Postulación actualizada con éxito";
                    }

                    return Ok(PostulacionesRespuesta);
                }
            }
            catch (SqlException ex)
            {
                return StatusCode(500, new { message = "Error al actualizar la Postulación en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado al actualizar la Postulación.", error = ex.Message });
            }
        }
        [AllowAnonymous]
        [Route("ActualizarPostulacion")]
        [HttpPut]
        public IActionResult ActualizarPostulacion(PostulacionEnt Postulacion)
        {
            PostulacionesRespuesta PostulacionesRespuesta = new PostulacionesRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("ActualizarPostulacion",
                        new
                        {
                            Postulacion.idEmpleado,
                            Postulacion.idPuesto,
                            Postulacion.FechaPostulacion,
                            Postulacion.estadoPostulacion
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        PostulacionesRespuesta.Codigo = "-1";
                        PostulacionesRespuesta.Mensaje = "No se pudo actualizar la postulación";
                        return BadRequest(PostulacionesRespuesta);
                    }
                    else
                    {
                        PostulacionesRespuesta.Codigo = "1";
                        PostulacionesRespuesta.Mensaje = "Postulación actualizada con éxito";
                        return Ok(PostulacionesRespuesta);
                    }
                }
            }
            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al actualizar la postulacion en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al actualizar la postulacion.", error = ex.Message });
            }
        }



        // Eliminar una Postulación
        [AllowAnonymous]
        [Route("EliminarPostulacion")]
        [HttpDelete]
        public IActionResult EliminarPostulacion ( long idPostulacion)
        {
            PostulacionesRespuesta PostulacionesRespuesta  = new PostulacionesRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                    var result = db.Execute("EliminarPostulacion",
                        new
                        {
                            idPostulacion
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <=  0)
                {
                    PostulacionesRespuesta.Codigo = "-1";
                    PostulacionesRespuesta.Mensaje = "No se pudo eliminar la Postulación ";
                        return BadRequest(PostulacionesRespuesta);
                    }
                else
                {
                    PostulacionesRespuesta.Codigo = "1";
                    PostulacionesRespuesta.Mensaje = "Postulación eliminada con exito.";
                        return Ok(PostulacionesRespuesta);
                    }
                }
            }


            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al eliminar la postulacion en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al eliminar la postulacion.", error = ex.Message });
            }

        }
    }
}


