using Microsoft.AspNetCore.Mvc;
using CasoPracticoAPI.DTO;
using Microsoft.Data.SqlClient;
using Dapper;
using InfoBretesAPI.Models;
using InfoBretesAPI.Entities;
using static InfoBretesAPI.Entities.UserEnt;

namespace CasoPracticoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // POST: api/users/login
        [HttpPost("login")]
        public IActionResult Login(LoginDto ent)
        {
            UserRespuesta resp = new UserRespuesta();

            using var context = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var result = context.Query<UserEnt>("ObtenerUsuario",
                         new { ent.Email, ent.Password },
                         commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

            if (result != null)
            {
                resp.Codigo = "1";
                resp.Mensaje = "OK";
                resp.Dato = result;
            }
            else
            {
                resp.Codigo = "0";
                resp.Mensaje = "El usuario no esta registrado o las credenciales son incorrectas.";

            }

            return Ok(resp);
        }

        // POST: api/users/register
        [HttpPost("register")]
        public IActionResult Register(UserEnt ent)
        {
            Respuesta resp = new Respuesta();

            ent.FechaRegistro = DateTime.Now;
            ent.IdTipo = 1;

            using var context = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var result = context.Execute("RegistrarUsuario",
                         new { ent.Nombre, ent.Email, ent.Password, ent.FechaRegistro, ent.IdTipo },
                         commandType: System.Data.CommandType.StoredProcedure);

            if (result > 0)
            {
                resp.Codigo = 1;
                resp.Mensaje = "OK";
                resp.Contenido = true;

                return Ok(resp);
            }
            else
            {
                resp.Codigo = 0;
                resp.Mensaje = "La informacion del usuario ya esta registrada.";
                resp.Contenido = false;

                return Ok(resp);
            }
        }

        // GET: api/users/profile
        [HttpGet("profile")]
        public IActionResult Profile(string email)
        {
            UserRespuesta resp = new UserRespuesta();

            using var context = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var result = context.Query<UserEnt>("ObtenerUsuarioPorEmail",
                         new { email },
                         commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

            if (result != null)
            {
                resp.Codigo = "1";
                resp.Mensaje = "OK";
                resp.Dato = result;
            }
            else
            {
                resp.Codigo = "0";
                resp.Mensaje = "El usuario no existe";
            }

            return Ok(resp);
        }

        // GET: api/users/update
        [HttpGet("update")]
        public IActionResult UserInfo(string email)
        {
            UserRespuesta resp = new UserRespuesta();

            using var context = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var result = context.Query<UserEnt>("ObtenerUsuarioPorEmail",
                         new { email },
                         commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

            if (result != null)
            {
                resp.Codigo = "1";
                resp.Mensaje = "OK";
                resp.Dato = result;
            }
            else
            {
                resp.Codigo = "0";
                resp.Mensaje = "El usuario no existe";
            }

            return Ok(resp);
        }

        // POST: api/users/update
        [HttpPost("update")]
        public IActionResult UpdateUser(UserEnt ent)
        {
            UserRespuesta resp = new UserRespuesta();

            using var context = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var result = context.Execute("ActualizarUsuario",
                         new { ent.IdUsuario, ent.Nombre, ent.Email, ent.Password, ent.FechaRegistro, ent.Direccion, ent.IdTipo },
                         commandType: System.Data.CommandType.StoredProcedure);

            if (result > 0)
            {
                resp.Codigo = "1";
                resp.Mensaje = "OK";
            }
            else
            {
                resp.Codigo = "0";
                resp.Mensaje = "El usuario no existe";
            }

            return Ok(resp);
        }

        // DELETE: api/users/delete
        [HttpDelete("delete")]
        public IActionResult DeleteUser(int IdUsuario)
        {
            UserRespuesta resp = new UserRespuesta();

            using var context = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            var result = context.Execute("EliminarUsuario",
                         new { IdUsuario },
                         commandType: System.Data.CommandType.StoredProcedure);

            if (result > 0)
            {
                resp.Codigo = "1";
                resp.Mensaje = "OK";
            }
            else
            {
                resp.Codigo = "0";
                resp.Mensaje = "El usuario no existe";
            }
            return Ok(resp);
        }
    }
}
