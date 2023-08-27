using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Teste1.Models
{
    public class Carro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int Ano { get; set; }
        public string?  Combustivel { get; set; }
        public string? Cor { get; set; }
        public string? Motor { get; set; }
        public int Potencia { get; set; }
    }
}