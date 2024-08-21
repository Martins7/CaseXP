using Case.Dominio.DTOs;
using Case.Dominio.Entidades;
using Case.Dominio.Enums;
using Case.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Mvc;

namespace Case.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioDto usuarioDto)
        {
            if (string.IsNullOrEmpty(usuarioDto.CpfCnpj))
                return BadRequest("Paremetro Obrigatorio: CPF/CNPJ");

            if (string.IsNullOrEmpty(usuarioDto.Senha))
                return BadRequest("Paremetro Obrigatorio: Senha");

            if (string.IsNullOrEmpty(usuarioDto.Nome))
                return BadRequest("Paremetro Obrigatorio: Nome");

            if (string.IsNullOrEmpty(usuarioDto.Email))
                return BadRequest("Paremetro Obrigatorio: Email");

            if (!Enum.IsDefined(typeof(PapelUsuario), usuarioDto.Papel))
                return BadRequest("Paremetro Obrigatorio: Papel");


            var existingUsuario = await _usuarioService.GetByCpfCnpjAsync(usuarioDto.CpfCnpj);
            if (existingUsuario != null)
            {
                return BadRequest("Usuario Já Cadastrado.");
            }

            var usuario = new Usuario
            {
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = usuarioDto.Senha,
                Papel = usuarioDto.Papel,
                CpfCnpj = usuarioDto.CpfCnpj 
            };

            await _usuarioService.CreateAsync(usuario);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuario(int id)
        {
            var usuario = await _usuarioService.GetByIdAsync(id);
            if (usuario == null) return NotFound();

            return Ok(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetAllAsync();
            return Ok(usuarios);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] UsuarioDto usuarioDto)
        {
            if (string.IsNullOrEmpty(usuarioDto.CpfCnpj))
                return BadRequest("Paremetro Obrigatorio: CPF/CNPJ");

            if (string.IsNullOrEmpty(usuarioDto.Senha))
                return BadRequest("Paremetro Obrigatorio: Senha");

            if (string.IsNullOrEmpty(usuarioDto.Nome))
                return BadRequest("Paremetro Obrigatorio: Nome");

            if (string.IsNullOrEmpty(usuarioDto.Email))
                return BadRequest("Paremetro Obrigatorio: Email");

            if (!Enum.IsDefined(typeof(PapelUsuario), usuarioDto.Papel))
                return BadRequest("Paremetro Obrigatorio: Papel");


            var existingUsuario = await _usuarioService.GetByCpfCnpjAsync(usuarioDto.CpfCnpj);
            if (existingUsuario == null)
            {
                return BadRequest("Usuario Não Cadastrado.");
            }

            var usuario = new Usuario
            {
                Id = id,
                Nome = usuarioDto.Nome,
                Email = usuarioDto.Email,
                Senha = usuarioDto.Senha,
                Papel = usuarioDto.Papel,
                CpfCnpj = usuarioDto.CpfCnpj
            };
            
            
            await _usuarioService.UpdateAsync(usuario);
            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {

            var existingUsuario = await _usuarioService.GetByIdAsync(id);
            if (existingUsuario == null)
            {
                return BadRequest("Usuario Não Cadastrado.");

            }
            await _usuarioService.DeleteAsync(id);
            return NoContent();
        }
    }

}
