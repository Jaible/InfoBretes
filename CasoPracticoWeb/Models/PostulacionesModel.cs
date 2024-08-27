using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Services;
using System.Net.Http.Json;
using InfoBretesAPI.Models;


namespace InfoBretesAPI.Models 
{
    public class PostulacionesModel (HttpClient _http, IConfiguration _configuration) : IPostulacionesModel
    {
       

        public PostulacionesRespuesta ConsultarUnaPostulacion(int idPuesto)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Postulaciones/ConsultarUnaPostulacion";
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<PostulacionesRespuesta>().Result;

            return null;
        }

        

            public PostulacionesRespuesta? CrearPostulacion(PostulacionEnt entidad)
            {
                string url = _configuration.GetSection("settings:UrlApi").Value + "api/Postulaciones/CrearPostulacion";

                JsonContent body = JsonContent.Create(entidad);
                var RespuestaApi = _http.PostAsync(url, body).Result;
                if (RespuestaApi.IsSuccessStatusCode)
                    return RespuestaApi.Content.ReadFromJsonAsync<PostulacionesRespuesta>().Result;
                return null;
            }

           

            public PostulacionesRespuesta? ConsultarPostulacionPorId(long idPostulacion)
            {
                string url = _configuration.GetSection("settings:UrlApi").Value + "api/Postulaciones/ConsultarPostulacionPorId?idPostulacion=" + idPostulacion;
                var resp = _http.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<PostulacionesRespuesta>().Result;

                return null;
            }

            public PostulacionesRespuesta? ActualizarunaPostulacion(long idPostulacion)
            {
                string url = _configuration.GetSection("settings:UrlApi").Value + "api/Postulaciones/ActualizarunaPostulacion?idPostulacion=" + idPostulacion;
                var resp = _http.GetAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<PostulacionesRespuesta>().Result;

                return null;
            }
            public PostulacionesRespuesta? ActualizarPostulacion(PostulacionEnt entidad)
            {
                string url = _configuration.GetSection("settings:UrlApi").Value + "api/Postulaciones/ActualizarPostulacion";
                JsonContent body = JsonContent.Create(entidad);
                var resp = _http.PutAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<PostulacionesRespuesta>().Result;

                return null;
            }


        public PostulacionesRespuesta? EliminarPostulacion(long idPostulacion)
            {
                string url = _configuration.GetSection("settings:UrlApi").Value + "api/Postulaciones/EliminarPostulacion?idPostulacion=" + idPostulacion;
                var resp = _http.DeleteAsync(url).Result;
                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<PostulacionesRespuesta>().Result;

                return null;
            }
        }
    }
    