// Models/SolicitudModel.cs
using System.ComponentModel.DataAnnotations;
using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Services;
using System.Text.Json;

namespace CasoPracticoWeb.Models
{
    public class Solicitud
    {
        public int idSolicitud { get; set; }
        public int idUsuario { get; set; }
        public int idPostulacion { get; set; }
        public DateTime fechaSolicitud { get; set; }
        public IFormFile Curriculum { get; set; }
        public IFormFile Otrosdocumentos { get; set; }
        public string EstadoPostulacion { get; set; }


    }
}