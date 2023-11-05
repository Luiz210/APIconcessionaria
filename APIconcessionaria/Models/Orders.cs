namespace APIconcessionaria.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public string Formadepagamento { get; set; }
        public int CarroId { get; set; }
        public int UserId { get; set; }
    }
}
