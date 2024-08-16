using Microsoft.EntityFrameworkCore;
using CasoPracticoWeb.Models;

namespace WebApp.Data
{
    public class SolicitudRepository : ISolicitudRepository
    {
        private readonly ApplicationDbContext _context;

        public SolicitudRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Solicitud>> GetSolicitudesAsync()
        {
            return await _context.Solicitudes.Include(s => s.Archivos).ToListAsync();
        }

        public async Task<Solicitud> GetSolicitudByIdAsync(int id)
        {
            return await _context.Solicitudes.Include(s => s.Archivos)
                                             .FirstOrDefaultAsync(s => s.IdSolicitud == id);
        }

        public async Task AddSolicitudAsync(Solicitud solicitud)
        {
            _context.Solicitudes.Add(solicitud);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSolicitudAsync(Solicitud solicitud)
        {
            _context.Entry(solicitud).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSolicitudAsync(int id)
        {
            var solicitud = await _context.Solicitudes.FindAsync(id);
            if (solicitud != null)
            {
                _context.Solicitudes.Remove(solicitud);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddArchivoAsync(Archivo archivo)
        {
            _context.Archivos.Add(archivo);
            await _context.SaveChangesAsync();
        }
    }
}