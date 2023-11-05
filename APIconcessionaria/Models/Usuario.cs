using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIconcessionaria.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? NomeUsuario { get; set; }

        public string Cpf { get; set; } 

        public string? Telefone { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Senha { get; set; }

        public int Tipo { get; set; } = 0; 
    }
}
