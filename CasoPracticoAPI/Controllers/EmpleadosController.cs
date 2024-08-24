using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CasoPracticoAPI.Entities;
using System.Data;
using System.Data.SqlClient;
using static CasoPracticoAPI.Entities.EmpleadosEnt;

namespace InfoBretesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadosController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmpleadosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("ConsultarEmpleado")]
        [HttpGet]
        public IActionResult ConsultarEmpleado(int idUsuario)
        {
            EmpleadosRespuesta resp = new EmpleadosRespuesta();

            using var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var result = db.Query<EmpleadosEnt>("ObtenerEmpleadoPorUsuario",
                    new { idUsuario },
                    commandType: CommandType.StoredProcedure).FirstOrDefault();

            if (result != null)
            {
                resp.Codigo = "1";
                resp.Mensaje = "Empleado consultado con exito";
                resp.Dato = result;
            } else
            {
                resp.Codigo = "0";
                resp.Mensaje = "No se encontro empleado alguno";
            }
            return Ok(resp);
        }

        [Route("RegistrarEmpleados")]
        [HttpPost]
        public IActionResult RegistrarEmpleados(EmpleadosEnt ent)
        {
            EmpleadosRespuesta resp = new EmpleadosRespuesta();

            using var db = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var result = db.Execute("RegistrarEmpleado",
                    new { ent.linkCurriculum, ent.fotoPerfil, ent.telefono, ent.idUsuario },
                    commandType: CommandType.StoredProcedure);

            if (result > 0)
            {
                resp.Codigo = "1";
                resp.Mensaje = "Empleado registrado con exito";
            }
            else
            {
                resp.Codigo = "0";
                resp.Mensaje = "No se pudo registrar";
            }
            return Ok(resp);
        }
    }
}
