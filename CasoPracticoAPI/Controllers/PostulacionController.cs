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
    public class PostulacionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public PostulacionController(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection { get { return new SqlConnection(_connectionString); } }


        //GET: api/Postulaciones
        [HttpGet]
        public IEnumerable<Postulacion> GetPostulaciones()
        {
            using (IDbConnection dbConnection = Connection)
            {
                string sQuery = "SELECT * FROM Postulaciones"; dbConnection.Open();
                return dbConnection.Query<Postulacion>(sQuery);
            }
        }

        // POST: api/Postulacion
        [HttpPost]
        public IActionResult PostPostulacion([FromBody] Postulacion postulacion)
        {
            if (postulacion == null)
            {
                return BadRequest("La postulación no puede ser nula.");
            }
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    string sQuery = "INSERT INTO Postulaciones (idEmpleado, idPuesto, fechaPostulacion, estadoPostulacion) " +
                        "VALUES (@IdEmpleado, @IdPuesto, @FechaPostulacion, @EstadoPostulacion)";
                    dbConnection.Open(); var result = dbConnection.Execute(sQuery, postulacion);
                    if (result > 0)
                    {
                        return Ok("Postulación agregada con éxito.");
                    }
                    else { return StatusCode(500, "Error al agregar la postulación."); }
                }
            }
            catch (Exception ex)
            {
                 return StatusCode(500,
                 $"Error en el servidor: {ex.Message}"); } }






            }
        }


