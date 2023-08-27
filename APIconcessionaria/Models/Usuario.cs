using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIconcessionaria.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string? NomeUsuario { get; set; }
        public string? Senha { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}