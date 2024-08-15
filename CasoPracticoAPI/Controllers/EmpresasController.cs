using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CasoPracticoAPI.Entities;
using System.Data;
using System.Data.SqlClient;
using static CasoPracticoAPI.Entities.EmpresasEnt;

namespace CasoPracticoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmpresasController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [AllowAnonymous]
        [Route("RegistrarEmpresas")]
        [HttpPost]
        public IActionResult RegistrarEmpresas(EmpresasEnt Empresas)
        {

            EmpresasRespuesta EmpresasRespuesta = new EmpresasRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var parametros = new
                    {
                        Empresas.idUsuario,
                        Empresas.nombreEmpresa,
                        Empresas.direccion,
                        Empresas.descripcion,
                        Empresas.sitioWeb,
                        Empresas.telefono,
                        Empresas.correo
                    };

                    var result = db.Execute("RegistrarEmpresa", parametros, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        EmpresasRespuesta.Codigo = "1";
                        EmpresasRespuesta.Mensaje = "Empresas registrado con éxito.";
                        return Ok(EmpresasRespuesta);
                    }
                    else
                    {
                        EmpresasRespuesta.Codigo = "-1";
                        EmpresasRespuesta.Mensaje = "No se pudo registrar el Empresas.";
                        return BadRequest(EmpresasRespuesta);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al registrar el Empresas.", error = ex.Message });
            }
        }
        [AllowAnonymous]
        [Route("ConsultarEmpresas")]
        [HttpGet]
        public IActionResult ConsultarEmpresas()
        {
            EmpresasRespuesta EmpresasRespuesta = new EmpresasRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var resultadoBD = db.Query<EmpresasEnt>("ConsultarEmpresa", new { }, commandType: CommandType.StoredProcedure).ToList();

                    if (resultadoBD == null || resultadoBD.Count == 0)
                    {
                        EmpresasRespuesta.Codigo = "-1";
                        EmpresasRespuesta.Mensaje = "No hay Empresas registrados.";
                    }
                    else
                    {
                        EmpresasRespuesta.Datos = resultadoBD;
                        EmpresasRespuesta.Codigo = "1";
                        EmpresasRespuesta.Mensaje = "Empresas consultados con éxito.";
                    }
                    return Ok(EmpresasRespuesta);
                }
            }
            catch (SqlException ex)
            {

                return StatusCode(500, new { message = "Error al consultar Empresas en la base de datos.", error = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error inesperado al consultar Empresas.", error = ex.Message });
            }
        }




    }
}


