using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Services;
using InfoBretesWeb.DTO;
using System.Text.Json;
using static CasoPracticoWeb.Entities.PostulacionesEnt;
using static CasoPracticoWeb.Entities.PostulacionDTO;

namespace CasoPracticoWeb.Models
{
    public class PostulacionesModel(HttpClient _http, IConfiguration _configuration) : IPostulacionesModel
    {


        public PostulacionesRespuesta? ConsultarUnaPostulacion(int idPuesto)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Postulaciones/ConsultarUnaPostulacion?idPuesto=" + idPuesto;
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<PostulacionesRespuesta>().Result;

            return null;

        }

        public PostulacionesRespuesta? CrearUnaPostulacion(PostulacionesDTO ent)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Postulaciones/CrearPostulacion";

            JsonContent body = JsonContent.Create(ent);
            var resp = _http.PostAsync(url, body).Result;

            if(resp.IsSuccessStatusCode)
            {
                return resp.Content.ReadFromJsonAsync<PostulacionesRespuesta>().Result;
            }

            return null;
        }

        public PostulacionesRespuesta? ConsultarPostulacionPorId(int idPostulacion) {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Postulaciones/ConsultarPostulacionPorId?idPostulacion=" + idPostulacion;
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<PostulacionesRespuesta>().Result;

            return null;
        }

        public PostulacionesRespuesta? ActualizarunaPostulacion(int idPostulacion) {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Postulaciones/ActualizarunaPostulacion?idPostulacion=" + idPostulacion;
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<PostulacionesRespuesta>().Result;

            return null;
        }
        public PostulacionesRespuesta? ActualizarPostulacion(PostulacionesEnt entidad) {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Postulaciones/ActualizarPostulacion";
            JsonContent body = JsonContent.Create(entidad);
            var resp = _http.PutAsync(url, body).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<PostulacionesRespuesta>().Result;

            return null;
        }


        public PostulacionesRespuesta? EliminarPostulacion(int idPostulacion) {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Postulaciones/EliminarPostulacion?idPostulacion=" + idPostulacion;
            var resp = _http.DeleteAsync(url).Result;
            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<PostulacionesRespuesta>().Result;

            return null;
        }

        public PostulacionDTORespuesta? ConsultarPostulacionPorEmpleado(int idEmpleado) {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Postulaciones/ConsultarPostulacionPorEmpleado?idEmpleado=" + idEmpleado;
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<PostulacionDTORespuesta>().Result;

            return null;
        }
    }
}