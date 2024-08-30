using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CasoPracticoAPI.Entities;
using System.Data;
using System.Data.SqlClient;
using static CasoPracticoAPI.Entities.PuestosTrabajoEnt;

namespace CasoPracticoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuestosTrabajoController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PuestosTrabajoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [AllowAnonymous]
        [Route("RegistrarPuestosTrabajo")]
        [HttpPost]
        public IActionResult RegistrarPuestosTrabajo(PuestosTrabajoEnt PuestosTrabajo)
        {

            PuestosTrabajoRespuesta PuestosTrabajoRespuesta = new PuestosTrabajoRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var parametros = new
                    {
                        PuestosTrabajo.idEmpresa,
                        PuestosTrabajo.titulo,
                        PuestosTrabajo.descripcion,
                        PuestosTrabajo.requisitos,
                        PuestosTrabajo.ubicacion,
                        PuestosTrabajo.salario,
                        PuestosTrabajo.tipoEmpleo
                    };

                    var result = db.Execute("RegistrarOferta", parametros, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        PuestosTrabajoRespuesta.Codigo = "1";
                        PuestosTrabajoRespuesta.Mensaje = "PuestosTrabajo registrado con éxito.";
                        return Ok(PuestosTrabajoRespuesta);
                    }
                    else
                    {
                        PuestosTrabajoRespuesta.Codigo = "-1";
                        PuestosTrabajoRespuesta.Mensaje = "No se pudo registrar el PuestosTrabajo.";
                        return BadRequest(PuestosTrabajoRespuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al registrar el PuestosTrabajo.", error = ex.Message });
            }
        }
        [AllowAnonymous]
        [Route("ConsultarPuestosTrabajo")]
        [HttpGet]
        public IActionResult ConsultarPuestosTrabajo()
        {
            PuestosTrabajoRespuesta PuestosTrabajoRespuesta = new PuestosTrabajoRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var resultadoBD = db.Query<PuestosTrabajoEnt>("ConsultarOferta", new { }, commandType: CommandType.StoredProcedure).ToList();

                    if (resultadoBD == null || resultadoBD.Count == 0)
                    {
                        PuestosTrabajoRespuesta.Codigo = "-1";
                        PuestosTrabajoRespuesta.Mensaje = "No hay Puestos de Trabajos registrados.";
                    }
                    else
                    {
                        PuestosTrabajoRespuesta.Datos = resultadoBD;
                        PuestosTrabajoRespuesta.Codigo = "1";
                        PuestosTrabajoRespuesta.Mensaje = "Puestos de Trabajos consultados con éxito.";
                    }
                    return Ok(PuestosTrabajoRespuesta);
                }
            }
            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al consultar PuestosTrabajo en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al consultar PuestosTrabajo.", error = ex.Message });
            }
        }


        [AllowAnonymous]
        [Route("ConsultarUnPuestoTrabajo")]
        [HttpGet]
        public IActionResult ConsultarUnPuestoTrabajo(int idEmpresa)
        {
            PuestosTrabajoRespuesta PuestosTrabajoRespuesta = new PuestosTrabajoRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Query<PuestosTrabajoEnt>("ObtenerOfertaPorId",
                        new { idEmpresa },
                        commandType: CommandType.StoredProcedure).ToList();

                    if (result == null)
                    {
                        PuestosTrabajoRespuesta.Codigo = "-1";
                        PuestosTrabajoRespuesta.Mensaje = "No hay PuestosTrabajo registrados.";
                    }
                    else
                    {
                        PuestosTrabajoRespuesta.Datos = result;
                        PuestosTrabajoRespuesta.Codigo = "1";
                        PuestosTrabajoRespuesta.Mensaje = "PuestosTrabajo consultado con éxito.";
                    }

                    return Ok(PuestosTrabajoRespuesta);
                }
            }
            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al consultar el PuestosTrabajo en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al consultar el PuestosTrabajo.", error = ex.Message });
            }
        }




        [AllowAnonymous]
        [Route("EliminarPuestosTrabajo")]
        [HttpDelete]
        public IActionResult EliminarPuestosTrabajo(long idPuesto)
        {
            PuestosTrabajoRespuesta PuestosTrabajoRespuesta = new PuestosTrabajoRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("EliminarOferta",
                        new
                        {
                            idPuesto
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        PuestosTrabajoRespuesta.Codigo = "-1";
                        PuestosTrabajoRespuesta.Mensaje = "No se ha podido eliminar el PuestosTrabajo en la base de datos, intenta de nuevo";
                        return BadRequest(PuestosTrabajoRespuesta);
                    }
                    else
                    {
                        PuestosTrabajoRespuesta.Codigo = "1";
                        PuestosTrabajoRespuesta.Mensaje = "PuestosTrabajo eliminado con éxito.";
                        return Ok(PuestosTrabajoRespuesta);
                    }
                }
            }
            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al eliminar el PuestosTrabajo en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al eliminar el PuestosTrabajo.", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [Route("ActualizarUnPuestosTrabajo")]
        [HttpGet]
        public IActionResult ActualizarUnPuestosTrabajo(long idPuesto)
        {
            PuestosTrabajoRespuesta PuestosTrabajoRespuesta = new PuestosTrabajoRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Query<PuestosTrabajoEnt>("ActualizarOfertaPorId",
                        new { idPuesto },
                        commandType: CommandType.StoredProcedure).FirstOrDefault();

                    if (result == null)
                    {
                        PuestosTrabajoRespuesta.Codigo = "-1";
                        PuestosTrabajoRespuesta.Mensaje = "No hay puestos registrados.";
                    }
                    else
                    {
                        PuestosTrabajoRespuesta.Dato = result;
                        PuestosTrabajoRespuesta.Codigo = "1";
                        PuestosTrabajoRespuesta.Mensaje = "Puesto consultado con éxito.";
                    }

                    return Ok(PuestosTrabajoRespuesta);
                }
            }
            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al consultar el proveedor en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al consultar el proveedor.", error = ex.Message });
            }
        }

        [AllowAnonymous]
        [Route("ActualizarPuestosTrabajo")]
        [HttpPut]
        public IActionResult ActualizarPuestosTrabajo(PuestosTrabajoEnt PuestosTrabajo)
        {
            PuestosTrabajoRespuesta PuestosTrabajoRespuesta = new PuestosTrabajoRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var result = db.Execute("ActualizarOferta",
                        new
                        {
                            PuestosTrabajo.idPuesto,
                            PuestosTrabajo.idEmpresa,
                            PuestosTrabajo.titulo,
                            PuestosTrabajo.descripcion,
                            PuestosTrabajo.requisitos
                        },
                        commandType: CommandType.StoredProcedure);

                    if (result <= 0)
                    {
                        PuestosTrabajoRespuesta.Codigo = "-1";
                        PuestosTrabajoRespuesta.Mensaje = "No se ha podido actualizar en la base de datos, intenta de nuevo";
                        return BadRequest(PuestosTrabajoRespuesta);
                    }
                    else
                    {
                        PuestosTrabajoRespuesta.Codigo = "1";
                        PuestosTrabajoRespuesta.Mensaje = "PuestosTrabajo actualizado con éxito.";
                        return Ok(PuestosTrabajoRespuesta);
                    }
                }
            }
            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al actualizar el PuestosTrabajo en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al actualizar el PuestosTrabajo.", error = ex.Message });
            }
        }



    }
}


