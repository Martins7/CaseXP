using Case.Dominio.DTOs;
using Case.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Case.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvestimentosController : ControllerBase
    {
        private readonly IInvestimentoService _investimentoService;
        private readonly ITransacaoService _transacaoService;

        public InvestimentosController(IInvestimentoService investimentoService, ITransacaoService transacaoService)
        {
            _investimentoService = investimentoService;
            _transacaoService = transacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var investimentos = await _investimentoService.GetAllAsync();
            return Ok(investimentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var investimento = await _investimentoService.GetByIdAsync(id);
            if (investimento == null)
            {
                return NotFound();
            }

            return Ok(investimento);
        }

        [HttpGet("cliente/{cpfCnpj}")]
        public async Task<IActionResult> GetInvestimentosByCpfCnpj(string cpfCnpj)
        {
            var investimentos = await _investimentoService.GetByClienteCpfCnpjAsync(cpfCnpj);
            if (investimentos == null)
            {
                return NotFound();
            }

            return Ok(investimentos);
        }

        [HttpPost("comprar")]
        public async Task<IActionResult> Comprar([FromBody] CompraVendaDto compraVendaDto)
        {
            if (compraVendaDto == null || compraVendaDto.Quantidade <= 0 || compraVendaDto.Preco <= 0)
            {
                return BadRequest("Dados de compra inválidos.");
            }

            try
            {
                await _investimentoService.ComprarInvestimentoAsync(compraVendaDto.InvestimentoId, compraVendaDto.ClienteId, compraVendaDto.Quantidade, compraVendaDto.Preco);
                return Ok("Compra realizada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao realizar a compra: {ex.Message}");
            }
        }

        [HttpPost("vender")]
        public async Task<IActionResult> Vender([FromBody] CompraVendaDto dto)
        {
            if (dto == null || dto.Quantidade <= 0 || dto.Preco <= 0)
            {
                return BadRequest("Dados de venda inválidos.");
            }

            try
            {
                await _investimentoService.VenderInvestimentoAsync(dto.InvestimentoId, dto.Quantidade, dto.Preco);
                return Ok("Venda realizada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao realizar a venda: {ex.Message}");
            }
        }

        [HttpGet("{id}/transacoes")]
        public async Task<IActionResult> GetTransacoes(int id)
        {
            var transacoes = await _transacaoService.GetTransacoesByInvestimentoIdAsync(id);
            return Ok(transacoes);
        }
    }
}
