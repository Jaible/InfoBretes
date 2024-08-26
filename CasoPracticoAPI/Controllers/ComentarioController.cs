using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CasoPracticoAPI.Entities;
using System.Data;
using System.Data.SqlClient;
using static CasoPracticoAPI.Entities.ComentarioEnt;

namespace CasoPracticoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ComentarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [Route("RegistrarComentario")]
        [HttpPost]
        public IActionResult RegistrarComentario(ComentarioEnt Comentario)
        {
            ComentarioRespuesta miRespuestaComentario = new ComentarioRespuesta();
            try
            {
                using (var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    var parametros = new
                    {
                        Comentario.idUsuario,
                        Comentario.idEmpresa,
                        Comentario.comentario
                    };

                    var result = db.Execute("RegistrarComentario", parametros, commandType: CommandType.StoredProcedure);
                    if (result > 0)
                    {
                        miRespuestaComentario.Codigo = "1";
                        miRespuestaComentario.Mensaje = "Comentario registrado con éxito.";
                        return Ok(miRespuestaComentario);
                    }
                    else
                    {
                        miRespuestaComentario.Codigo = "-1";
                        miRespuestaComentario.Mensaje = "No se pudo registrar el Comentario.";
                        return BadRequest(miRespuestaComentario);
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error al registrar el Comentario.", error = ex.Message });
            }
        }
    }
}


