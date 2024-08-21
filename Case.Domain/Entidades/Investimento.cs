namespace Case.Dominio.Entidades
{
    public class Investimento
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public DateTime DataCompra { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}
