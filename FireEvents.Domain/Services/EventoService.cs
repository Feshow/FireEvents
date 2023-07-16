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
        public async Task<Evento?> AddEvento(Evento model)
        {
            try
            {
                if (await _eventoRepository.AddAsync(model))
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
        public async Task<Evento?> UpdateEvento(int id, Evento model)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(model.Id, false);
                if (evento != null)
                {
                    await _eventoRepository.Update(model);
                    if (await _eventoRepository.SaveAsync())
                    {
                        return await _eventoRepository.GetEventoByIdAsync(model.Id, false);
                    }
                }                   

                return null;

            }
            catch (Exception ex)
            {
                throw new Exception($@"Erro ao inserir novo evento: {ex.Message}");
            }
        }
        public async Task<bool> DeleteEvento(int eventoId)
        {
            try
            {
                var evento = await _eventoRepository.GetEventoByIdAsync(eventoId);
                if (evento != null)
                {
                    await _eventoRepository.DeleteAsync(evento);
                    return await _eventoRepository.SaveAsync();
                }

                return false;

            }
            catch (Exception ex)
            {
                throw new Exception($@"Erro ao inserir novo evento: {ex.Message}");
            }
        }
        public async Task<IEnumerable<Evento>?> ObterEventosAsync(bool includePalestrantes)
        {
            try
            {
                var eventos = await _eventoRepository.GetAllEventosAsync(includePalestrantes);
                if (eventos == null)
                    return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception($@"Erro ao obter eventos: {ex.Message}");
            }
        }
        public async Task<Evento?> ObterEventoPorIdAsync(int idEvent, bool includePalestrantes)
        {
            try
            {
                var eventos = await _eventoRepository.GetEventoByIdAsync(idEvent, includePalestrantes);
                if (eventos == null)
                    return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception($@"Erro ao obter eventos: {ex.Message}");
            }
        }
        public async Task<IEnumerable<Evento>?> ObterEventoPorTemaAsync(string tema, bool includePalestrantes)
        {
            try
            {
                IEnumerable<Evento>? eventos = await _eventoRepository.GetEventosByTemaAsync(tema, includePalestrantes);
                if (eventos == null)
                    return null;

                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception($@"Erro ao obter eventos: {ex.Message}");
            }
        }
    }
}
