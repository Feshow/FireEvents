using FireEvents.Domain.Entities;

namespace FireEvents.Domain.Interfaces.IRepository
{
    public interface IEventoRepository : IBaseRepository<Evento>
    {
        Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false);
        Task<Evento[]> GetEventosByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento> GetEventoByIdAsync(int id, bool includePalestrantes = false);

    }
}
