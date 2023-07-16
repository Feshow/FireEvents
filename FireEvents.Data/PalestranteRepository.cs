using FireEvents.Data.Contexts;
using FireEvents.Domain.Entities;
using FireEvents.Domain.Interfaces.IRepository;
using Microsoft.EntityFrameworkCore;

namespace FireEvents.Data
{
    public class PalestranteRepository : BaseRepository<Palestrante>, IPalestranteRepository
    {
        private readonly ApplicationDbContext _db;
        public PalestranteRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(bool includePalestrantes, bool tracked)
        {
            IQueryable<Palestrante> query = _db.Palestrantes
                .Include(p => p.RedesSociais);

            if (!tracked)
                query = query.AsNoTracking();

            if (includePalestrantes)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Evento);
            }

            query.OrderBy(p => p.Id);
            return await query.ToArrayAsync();
        }
        public async Task<Palestrante[]> GetPalestrantesByNomeAsync(string nome, bool includePalestrantes, bool tracked)
        {
            IQueryable<Palestrante> query = _db.Palestrantes
                .Include(p => p.RedesSociais);
            
            if (!tracked)
                query = query.AsNoTracking();

            if (includePalestrantes)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query.OrderBy(p => p.Id).Where(e => e.Nome.ToLower().Contains(nome.ToLower()));
            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int id, bool includePalestrante, bool tracked)
        {
            IQueryable<Palestrante> query = _db.Palestrantes
                .Include(p => p.RedesSociais);

            if (!tracked)
                query = query.AsNoTracking();

            if (includePalestrante)
            {
                query = query
                    .Include(p => p.PalestrantesEventos)
                    .ThenInclude(pe => pe.Palestrante);
            }

            query.OrderBy(p => p.Id).Where(e => e.Id == id);
            return await query.FirstOrDefaultAsync();
        }
    }
}
