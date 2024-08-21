namespace Case.Dominio.Entidades
{
    public class Carteira
    {
        public Carteira()
        {
            Investimentos = new List<Investimento>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public List<Investimento> Investimentos { get; set; }
    }
}
