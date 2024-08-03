using Dapper;
using InfoBretesApi.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace InfoBretesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresasController(IConfiguration iConfiguration) : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("RegistrarEmpresas")]
        public async Task<IActionResult> RegistrarEmpresas(Empresas ent)
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value))
            {
                var result = await context.ExecuteAsync("RegistrarEmpresas",
                    new { ent.nombreEmpresa, ent.direccion, ent.descripcion, ent.sitioWeb, ent.telefono },
                    commandType: System.Data.CommandType.StoredProcedure);

                if(result > 0)
                {
                    resp.Codigo = 1;
                    resp.Mensaje = "La empresa fue agregada con exito";
                    resp.Contenido = true;
                    return Ok(resp);
                }
                else
                {
                    resp.Codigo = 0;
                    resp.Mensaje = "La información de la empresa ya ha sido registrada";
                    resp.Contenido = false;
                    return Ok(resp);
                }
            }
        }
    }
}
