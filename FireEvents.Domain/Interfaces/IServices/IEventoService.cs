using FireEvents.Domain.Entities;

namespace FireEvents.Domain.Interfaces.IServices
{
    public interface IEventoService
    {
        Task<Evento?> AddEvento(Evento model);
        Task<Evento?> UpdateEvento(int id, Evento model);
        Task<bool> DeleteEvento(int eventoId);
        Task<IEnumerable<Evento>?> ObterEventosAsync(bool includePalestrantes = false);
        Task<Evento?> ObterEventoPorIdAsync(int idEvent, bool includePalestrantes = false);
        Task<IEnumerable<Evento>?> ObterEventoPorTemaAsync(string tema, bool includePalestrantes = false);
    }
}
