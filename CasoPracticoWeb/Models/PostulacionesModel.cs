﻿using CasoPracticoWeb.Entities;
using CasoPracticoWeb.Services;
using System.Text.Json;
using static CasoPracticoWeb.Entities.PostulacionesEnt;

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

    }
}