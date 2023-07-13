using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FireEvents.Domain.Entities
{
    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Tema { get; set; }
        public string? Endereco { get; set; }
        public DateTime? DataEvento { get; set; }
        public int QtdPessoas { get; set; }
        public string? ImagemUrl { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public IEnumerable<Lote>? Lotes { get; set; }
        public IEnumerable<RedeSocial>? RedesSociais { get; set; }
        public IEnumerable<PalestranteEvento>? PalestrantesEventos { get; set; }
    }
}
