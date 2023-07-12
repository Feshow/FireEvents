using FireEvents.Domain.Entities;

namespace FireEvents.Domain.Interfaces.IServices
{
    public interface IEventoService
    {
        Task<Evento> AddEvento(Evento model);
        Task<Evento> UpdateEvento(Evento model);
        Task<bool> DeleteEvento(int eventoId);
    }
}
