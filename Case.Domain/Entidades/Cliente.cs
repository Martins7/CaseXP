namespace Case.Dominio.Entidades
{
    public class Cliente 
    {
        public int Id { get; set; }
        public string CpfCnpj { get; set; }
        public List<Investimento> Investimentos { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
