using CasoPracticoWeb.Models;

namespace WebApp.Data
{
    public interface ISolicitudRepository
    {
        Task<IEnumerable<Solicitud>> GetSolicitudesAsync();
        Task<Solicitud> GetSolicitudByIdAsync(int id);
        Task AddSolicitudAsync(Solicitud solicitud);
        Task UpdateSolicitudAsync(Solicitud solicitud);
        Task DeleteSolicitudAsync(int id);
        Task AddArchivoAsync(Archivo archivo);
    }
}
