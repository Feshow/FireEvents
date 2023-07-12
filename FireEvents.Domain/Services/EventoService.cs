using FireEvents.Domain.Entities;
using FireEvents.Domain.Interfaces.IRepository;
using FireEvents.Domain.Interfaces.IServices;

namespace FireEvents.Domain.Services
{
    public class EventoService : IEventoService
    {
        private readonly IEventoRepository _eventoRepository;
        public EventoService(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }
        public async Task<Evento> AddEvento(Evento model)
        {
            try
            {
                await _eventoRepository.AddAsync(model);
                if (await _eventoRepository.SaveAsync())
                {
                    return await _eventoRepository.GetEventoByIdAsync(model.Id);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($@"Erro ao inserir novo evento: {ex.Message}");
            }
        }

        public Task<Evento> UpdateEvento(Evento model)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteEvento(int eventoId)
        {
            throw new NotImplementedException();
        }
    }
}
