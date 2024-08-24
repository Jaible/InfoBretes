using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CasoPracticoAPI.Entities;
using System.Data;
using System.Data.SqlClient;
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
    }
}


