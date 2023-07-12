using FireEvents.Domain.Entities;

namespace FireEvents.Domain.Interfaces.IRepository
{
    public interface IPalestranteRepository : IBaseRepository<Palestrante>
    {
        Task<Palestrante[]> GetAllPalestrantesAsync(bool includePalestrantes = false);
        Task<Palestrante[]> GetPalestrantesByNomeAsync(string nome, bool includePalestrantes = false);
        Task<Palestrante> GetPalestranteByIdAsync(int id, bool includePalestrante = false);
    }
}
