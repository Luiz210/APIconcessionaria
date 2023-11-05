using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIconcessionaria.Models
{
    public class Carro
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public int Ano { get; set; }
        public string?  Combustivel { get; set; }
        public string? Cor { get; set; }
        public string? Motor { get; set; }
        public int Potencia { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }
    }
}