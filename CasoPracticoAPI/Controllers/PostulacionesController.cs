using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CasoPracticoAPI.Entities;
using System.Data;
using System.Data.SqlClient;
using static CasoPracticoAPI.Entities.PostulacionesEnt;

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




    }
}


