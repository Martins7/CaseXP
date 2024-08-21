using Case.Dominio.Enums;

namespace Case.Dominio.DTOs
{
    public class UsuarioDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public PapelUsuario Papel { get; set; }
        public string CpfCnpj { get; set; } 
    }
}
