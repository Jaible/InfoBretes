using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Services;
using static CasoPracticoWeb.Entities.ComentarioEnt;


namespace CasoPracticoWeb.Models
{
    public class ComentarioModel(HttpClient _http, IConfiguration _configuration) : IComentarioModel
    {

        public ComentarioRespuesta? RegistrarComentario(ComentarioEnt entidad)
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Comentario/RegistrarComentario";

            JsonContent body = JsonContent.Create(entidad);
            var RespuestaApi = _http.PostAsync(url, body).Result;
            if (RespuestaApi.IsSuccessStatusCode)
                return RespuestaApi.Content.ReadFromJsonAsync<ComentarioRespuesta>().Result;
            return null;
        }

        public ComentarioRespuesta? ConsultarComentario()
        {
            string url = _configuration.GetSection("settings:UrlApi").Value + "api/Empresas/ConsultarEmpresas";
            var resp = _http.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
                return resp.Content.ReadFromJsonAsync<ComentarioRespuesta>().Result;

            return null;
        }

    }
}