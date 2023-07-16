using FireEvents.Data.Contexts;
using FireEvents.Domain.Entities;
using FireEvents.Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FireEvents.Data
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRepository
    {
        private readonly ApplicationDbContext _db;
        public EventoRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Evento>?> GetAllEventosAsync(bool includePalestrantes, bool tracked)
        {
            IQueryable<Evento> query = _db.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (!tracked)            
                query = query.AsNoTracking();            

            if (includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query.OrderBy(e => e.Id);
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Evento>?> GetEventosByTemaAsync(string tema, bool includePalestrantes, bool tracked)
        {
            IQueryable<Evento> query = _db.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (!tracked)            
                query = query.AsNoTracking();            


            if (includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));
            return await query.ToListAsync();
        }

        public async Task<Evento?> GetEventoByIdAsync(int id, bool includePalestrantes, bool tracked)
        {
            IQueryable<Evento> query = _db.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if (!tracked)
                query = query.AsNoTracking();

            if (includePalestrantes)
            {
                query = query
                    .Include(e => e.PalestrantesEventos)
                    .ThenInclude(p => p.Palestrante);
            }

            return await query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id == id).FirstOrDefaultAsync();
        }
    }
}
