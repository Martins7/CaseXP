using Case.Dominio.Enums;
using Microsoft.AspNetCore.Identity;

namespace Case.Dominio.Entidades
{
    public class Usuario 
    {
        public int Id { get; set; }
        public string CpfCnpj { get; set; } 
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public PapelUsuario Papel { get; set; }
    }
}
