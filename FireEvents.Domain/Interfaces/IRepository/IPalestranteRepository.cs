using FireEvents.Domain.Entities;

namespace FireEvents.Domain.Interfaces.IRepository
{
    public interface IPalestranteRepository : IBaseRepository<Palestrante>
    {
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includePalestrantes = false, bool tracked = false);
        Task<Palestrante[]> GetPalestrantesByNomeAsync(string nome, bool includePalestrantes = false, bool tracked = false);
        Task<Palestrante> GetPalestranteByIdAsync(int id, bool includePalestrante = false, bool tracked = false);
    }
}
