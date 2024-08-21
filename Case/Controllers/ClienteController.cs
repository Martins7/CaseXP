using Case.Dominio.DTOs;
using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Mvc;


namespace Case.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IUsuarioService _usuarioService;

        public ClienteController(IClienteService clienteRepository, IUsuarioService usuarioService)
        {
            _clienteService = clienteRepository;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var clientes = await _clienteService.GetAllAsync();
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteService.GetByIdAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> Cadastrar([FromBody] ClienteDTO clienteDto)
        {
            if(string.IsNullOrEmpty(clienteDto.CpfCnpj))
                return BadRequest("Paremetro Obrigatorio: CPF/CNPJ");

            var existingCliente = await _clienteService.GetByCpfCnpjAsync(clienteDto.CpfCnpj);
            if (existingCliente != null)
            {
                return BadRequest("Cliente Já Cadastrado.");
            }

            var usuario = await _usuarioService.GetByCpfCnpjAsync(clienteDto.CpfCnpj);
            if (usuario != null)
            {
                return BadRequest($"Cliente Do CPF/CNPJ não possui usuario cadastrado.");
            }

            var cliente = new Cliente
            {
                CpfCnpj = clienteDto.CpfCnpj,
                Investimentos = new List<Investimento>(),
                UsuarioId = usuario.Id,
            };

            await _clienteService.AddAsync(cliente);
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.Id }, cliente);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] ClienteDTO clienteDto)
        {
            var existingCliente = await _clienteService.GetByCpfCnpjAsync(clienteDto.CpfCnpj);
            if (existingCliente == null)
            {
                return BadRequest("Cliente Não Cadastrado.");
            }

            var usuario = await _usuarioService.GetByCpfCnpjAsync(clienteDto.CpfCnpj);
            if (usuario != null)
            {
                return BadRequest($"Cliente Do CPF/CNPJ não possui usuario cadastrado.");
            }

            var cliente = new Cliente
            {
                Id = id,
                CpfCnpj = clienteDto.CpfCnpj,
                Investimentos = existingCliente.Investimentos,
            };

            await _clienteService.UpdateAsync(cliente);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var existingCliente = await _clienteService.GetByIdAsync(id);
            if (existingCliente == null)
            {
                return BadRequest("Cliente Não Cadastrado.");
            }

            await _clienteService.DeleteAsync(id);
            return Ok();
        }
    }
}
