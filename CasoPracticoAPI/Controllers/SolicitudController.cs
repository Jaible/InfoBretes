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
using System.Data;

namespace CasoPracticoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudesApiController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;
        private readonly ILogger<SolicitudesApiController> _logger;

        public SolicitudesApiController(IDbConnection dbConnection, ILogger<SolicitudesApiController> logger)
        {
            _dbConnection = dbConnection;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetSolicitudes()
        {
            try
            {
                var sql = "SELECT * FROM Solicitudes";
                var solicitudes = await _dbConnection.QueryAsync<Solicitud>(sql);
                return Ok(solicitudes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de solicitudes.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSolicitud(int id)
        {
            try
            {
                var sql = "SELECT * FROM Solicitudes WHERE IdSolicitud = @Id";
                var solicitud = await _dbConnection.QuerySingleOrDefaultAsync<Solicitud>(sql, new { Id = id });
                if (solicitud == null)
                {
                    return NotFound();
                }
                return Ok(solicitud);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener los detalles de la solicitud con ID {id}.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSolicitud([FromBody] Solicitud solicitud)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                solicitud.FechaSolicitud = DateTime.Now;

                var sql = "INSERT INTO Solicitudes (Nombre, EstadoPostulacion, FechaSolicitud) VALUES (@Nombre, @EstadoPostulacion, @FechaSolicitud)";
                await _dbConnection.ExecuteAsync(sql, solicitud);
                var solicitudId = _dbConnection.QuerySingle<int>("SELECT LAST_INSERT_ID();");

                return CreatedAtAction(nameof(GetSolicitud), new { id = solicitudId }, solicitud);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la solicitud.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSolicitud(int id, [FromBody] Solicitud solicitud)
        {
            if (id != solicitud.IdSolicitud)
            {
                return BadRequest("El ID de la solicitud no coincide.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var sql = "UPDATE Solicitudes SET Nombre = @Nombre, EstadoPostulacion = @EstadoPostulacion WHERE IdSolicitud = @Id";
                await _dbConnection.ExecuteAsync(sql, new { solicitud.Nombre, solicitud.EstadoPostulacion, Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar la solicitud con ID {id}.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitud(int id)
        {
            try
            {
                var sql = "DELETE FROM Solicitudes WHERE IdSolicitud = @Id";
                await _dbConnection.ExecuteAsync(sql, new { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la solicitud con ID {id}.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}