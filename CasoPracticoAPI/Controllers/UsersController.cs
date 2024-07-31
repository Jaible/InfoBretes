using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CasoPracticoAPI.DTO;
using Microsoft.Data.SqlClient;
using Dapper;
using InfoBretesAPI.Models;

namespace CasoPracticoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        // POST: api/users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto ent)
        {
            Respuesta resp = new Respuesta();

            using var context = new SqlConnection("Server=JAIBLE; Database=InfoBretes; Trusted_Connection=True; TrustServerCertificate=True;");
            var result = await context.QueryAsync<User>("ObtenerUsuario",
                         new { ent.Email },
                         commandType: System.Data.CommandType.StoredProcedure);

            if (result.Any())
            {
                resp.Codigo = 1;
                resp.Mensaje = "OK";
                resp.Contenido = result;

                return Ok(resp);
            }
            else
            {
                resp.Codigo = 0;
                resp.Mensaje = "El usuario no esta registrado o las credenciales son incorrectas.";
                resp.Contenido = false;

                return Ok(resp);
            }
        }

        // POST: api/users/register
        [HttpPost("register")]
        public IActionResult Register(User ent)
        {
            Respuesta resp = new Respuesta();

            ent.FechaRegistro = DateTime.Now;
            ent.IdTipo = 1;

            using var context = new SqlConnection("Server=JAIBLE; Database=InfoBretes; Trusted_Connection=True; TrustServerCertificate=True;");
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
    }
}
