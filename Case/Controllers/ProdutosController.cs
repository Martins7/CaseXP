using Case.Dominio.DTOs;
using Case.Dominio.Entidades;
using Case.Dominio.Interfaces.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Case.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var produtos = await _produtoService.GetAllAsync();
            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _produtoService.GetByIdAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProdutoDto produtoDto)
        {
            if (produtoDto == null)
                return BadRequest("Dados do produto inválidos.");

            if (string.IsNullOrEmpty(produtoDto.Nome))
                return BadRequest("Parâmetros obrigatórios: Nome.");
            if (!DateTime.TryParse(produtoDto.DataVencimento.ToString(), out DateTime _))
                return BadRequest("A Data de Vencimento está em um formato invalido.");
            if (produtoDto.DataVencimento > DateTime.Now)
                return BadRequest("A Data de Vencimento deve ser uma data futura");
            if (produtoDto.Valor <= 0)
                return BadRequest("O Valor deve ser maior que zero.");


            var produto = new Produto
            {
                Nome = produtoDto.Nome,
                TipoProduto = produtoDto.TipoProduto,
                DataVencimento = produtoDto.DataVencimento,
                Valor = produtoDto.Valor,
                Disponivel = produtoDto.Disponivel
            };

            await _produtoService.AddAsync(produto);
            return CreatedAtAction(nameof(GetById), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProdutoDto produtoDto)
        {
            if (produtoDto == null)
                return BadRequest("Dados do produto inválidos.");

            if (!DateTime.TryParse(produtoDto.DataVencimento.ToString(), out DateTime _))
                return BadRequest("A Data de Vencimento está em um formato invalido.");

            if (produtoDto.Valor <= 0)
                return BadRequest("O Valor deve ser maior que zero.");

            var existingProduto = await _produtoService.GetByIdAsync(id);

            if (existingProduto == null)
                return BadRequest("Produto Não cadastrado");

            var produto = new Produto
            {
                Id = id,
                DataVencimento = produtoDto.DataVencimento,
                Disponivel = produtoDto.Disponivel,
                Nome = produtoDto.Nome,
                TipoProduto = produtoDto.TipoProduto,
                Valor = produtoDto.Valor
            };


            await _produtoService.UpdateAsync(produto);
            return Ok("Produto atualizado com sucesso.");
        }

        [HttpPost("{id}/desativar")]
        public async Task<IActionResult> Deactivate(int id)
        {

            try
            {
                await _produtoService.DesativarProdutoAsync(id);
                return Ok("Produto desativado com sucesso.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Produto não encontrado.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao desativar o produto: {ex.Message}");
            }
        }
    }
}
