using InfoBreteWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Diagnostics;

namespace InfoBreteWeb.Controllers
{
    public class HomeController(IEmpresasModel iEmpresaModel) : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegistrarEmpresas()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarEmpresas(EmpresasModel ent)
        {
            var respuesta = iEmpresaModel.RegistrarEmpresas(ent);
            return View();
        }
    }
}
