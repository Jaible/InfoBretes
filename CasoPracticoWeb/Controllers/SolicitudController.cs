using Microsoft.AspNetCore.Mvc;
using CasoPracticoWeb.Models;
using CasoPracticoWeb.Services;
using System.Data;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebApp.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace CasoPracticoWeb.Controllers
{
    public class SolicitudesController : Controller
    {
        private readonly ISolicitudRepository _solicitudRepository;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly ITempDataProvider _tempDataProvider;
        private readonly ILogger<SolicitudesController> _logger;
        private Uri baseAdrress = new Uri("https://localhost:7150/api/Users/");
        private readonly HttpClient _httpClient;
        public SolicitudesController(ISolicitudRepository solicitudRepository, ILogger<SolicitudesController> logger, ICompositeViewEngine viewEngine, ITempDataProvider tempDataProvider)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            solicitudRepository = solicitudRepository;
            _viewEngine = viewEngine;
            _tempDataProvider = tempDataProvider;
        }




        public async Task<IActionResult> Index()
        {
            try
            {
                var solicitudes = await _solicitudRepository.GetSolicitudesAsync();
                return View(solicitudes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la lista de solicitudes.");
                ModelState.AddModelError("", "No se pudo cargar la lista de solicitudes. Intente nuevamente más tarde.");
                return View(new List<Solicitud>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var solicitud = await _solicitudRepository.GetSolicitudByIdAsync(id);
                if (solicitud == null)
                {
                    return NotFound();
                }
                return View(solicitud);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener los detalles de la solicitud con ID {id}.");
                ModelState.AddModelError("", "No se pudo cargar los detalles de la solicitud. Intente nuevamente más tarde.");
                return View();
            }
        }

        // Método para obtener la lista de estados de postulacion
        private SelectList GetEstadosSelectList()
        {
            var estados = new List<SelectListItem>
    {
        new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
        new SelectListItem { Value = "Aceptada", Text = "Aceptada" },
        new SelectListItem { Value = "Rechazada", Text = "Rechazada" }
    };

            return new SelectList(estados, "Value", "Text");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Estados = GetEstadosSelectList();
            var solicitud = new Solicitud
            {
                EstadoPostulacion = "Pendiente" // Valor predeterminado
            };
            return View(solicitud);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Solicitud solicitud, IFormFile curriculumFile, List<IFormFile> otrosDocumentosFiles)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    solicitud.fechaSolicitud = DateTime.Now;
                    await _solicitudRepository.AddSolicitudAsync(solicitud);

                    if (curriculumFile != null)
                    {
                        var archivo = new Archivo
                        {
                            IdSolicitud = solicitud.idSolicitud,
                            NombreArchivo = curriculumFile.FileName,
                            DatosArchivo = await ConvertToBytesAsync(curriculumFile)
                        };
                        await _solicitudRepository.AddArchivoAsync(archivo);
                    }

                    if (otrosDocumentosFiles != null && otrosDocumentosFiles.Count > 0)
                    {
                        foreach (var file in otrosDocumentosFiles)
                        {
                            var archivo = new Archivo
                            {
                                IdSolicitud = solicitud.idSolicitud,
                                NombreArchivo = file.FileName,
                                DatosArchivo = await ConvertToBytesAsync(file)
                            };
                            await _solicitudRepository.AddArchivoAsync(archivo);
                        }
                    }

                    return Json(new { success = true, redirectUrl = Url.Action("Index") });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al crear la solicitud.");
                    ModelState.AddModelError("", "No se pudo crear la solicitud. Intente nuevamente más tarde.");
                }
            }

            ViewBag.Estados = GetEstadosSelectList();
            var partialView = await RenderViewToStringAsync("Create", solicitud);
            return Json(new { success = false, html = partialView });
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var solicitud = await _solicitudRepository.GetSolicitudByIdAsync(id);
                if (solicitud == null)
                {
                    return NotFound();
                }

                ViewBag.Estados = GetEstadosSelectList();
                return View(solicitud);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cargar la solicitud para edición con ID {id}.");
                ModelState.AddModelError("", "No se pudo cargar la solicitud para edición. Intente nuevamente más tarde.");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Solicitud solicitud, IFormFile curriculumFile, List<IFormFile> otrosDocumentosFiles)
        {
            if (id != solicitud.idSolicitud)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (curriculumFile != null)
                    {
                        var archivo = new Archivo
                        {
                            IdSolicitud = solicitud.idSolicitud,
                            NombreArchivo = curriculumFile.FileName,
                            DatosArchivo = await ConvertToBytesAsync(curriculumFile)
                        };
                        await _solicitudRepository.AddArchivoAsync(archivo);
                    }

                    if (otrosDocumentosFiles != null && otrosDocumentosFiles.Count > 0)
                    {
                        foreach (var file in otrosDocumentosFiles)
                        {
                            var archivo = new Archivo
                            {
                                IdSolicitud = solicitud.idSolicitud,
                                NombreArchivo = file.FileName,
                                DatosArchivo = await ConvertToBytesAsync(file)
                            };
                            await _solicitudRepository.AddArchivoAsync(archivo);
                        }
                    }

                    await _solicitudRepository.UpdateSolicitudAsync(solicitud);
                    return Json(new { success = true, redirectUrl = Url.Action("Index") });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error al actualizar la solicitud con ID {id}.");
                    ModelState.AddModelError("", "No se pudo actualizar la solicitud. Intente nuevamente más tarde.");
                }
            }

            ViewBag.Estados = GetEstadosSelectList();
            var partialView = await RenderViewToStringAsync("Edit", solicitud);
            return Json(new { success = false, html = partialView });
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var solicitud = await _solicitudRepository.GetSolicitudByIdAsync(id);
                if (solicitud == null)
                {
                    return NotFound();
                }

                return View(solicitud);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cargar la solicitud para eliminación con ID {id}.");
                ModelState.AddModelError("", "No se pudo cargar la solicitud para eliminación. Intente nuevamente más tarde.");
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _solicitudRepository.DeleteSolicitudAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar la solicitud con ID {id}.");
                ModelState.AddModelError("", "No se pudo eliminar la solicitud. Intente nuevamente más tarde.");
                return RedirectToAction(nameof(Index));
            }
        }

        private async Task<byte[]> ConvertToBytesAsync(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        private async Task<string> RenderViewToStringAsync(string viewName, object model)
        {
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model
            };

            using (var writer = new StringWriter())
            {
                var viewResult = _viewEngine.FindView(ControllerContext, viewName, false);
                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }
                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    viewData,
                    TempData,
                    writer,
                    new HtmlHelperOptions()
                );
                await viewResult.View.RenderAsync(viewContext);
                return writer.GetStringBuilder().ToString();
            }
        }
    }
}