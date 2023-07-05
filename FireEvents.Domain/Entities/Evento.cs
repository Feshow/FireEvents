using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FireEvents.Domain.Entities
{
    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Tema { get; set; }
        public string Endereco { get; set; }
        public DateTime DataEvento { get; set; }
        public int QtdPessoas { get; set; }
        public int Lote { get; set; }
        public string ImagemUrl { get; set; }
    }
}
