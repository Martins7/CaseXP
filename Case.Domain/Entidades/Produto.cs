using Case.Dominio.Enums;

namespace Case.Dominio.Entidades
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoProduto TipoProduto { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public bool Disponivel { get; set; }
    }
}
