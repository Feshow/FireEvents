using FireEvents.Domain.Entities;

namespace FireEvents.Domain.Interfaces.IRepository
{
    public interface IEventoRepository : IBaseRepository<Evento>
    {
        Task<IEnumerable<Evento>?> GetAllEventosAsync(bool includePalestrantes = false, bool tracked = false);
        Task<IEnumerable<Evento>?> GetEventosByTemaAsync(string tema, bool includePalestrantes = false, bool tracked = false);
        Task<Evento?> GetEventoByIdAsync(int id, bool includePalestrantes = false, bool tracked = false);

    }
}
