using APIconcessionaria.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class ImagemCarro
{
    [Key]
    [ForeignKey("Carro")]
    public int CarroId { get; set; }
    [Column(TypeName = "VARBINARY(MAX)")] 
    public byte[]? NomeArquivo { get; set; }
    [ForeignKey("CarroId")]
    public Carro? Carro { get; set; }
}
