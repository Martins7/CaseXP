using Case.Dominio.Entidades;
using Case.Dominio.Enums;
using Case.Dominio.Interfaces.Repositorios;
using Case.Dominio.Interfaces.Servicos;
using Case.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Servicos
{
    public class InvestimentoService : IInvestimentoService
    {
        private readonly IInvestimentoRepository _investimentoRepository;
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IProdutoRepository _produtoRepository;

        public InvestimentoService(IInvestimentoRepository investimentoRepository, IClienteRepository clienteRepository, IProdutoRepository produtoRepository = null, ITransacaoRepository transacaoRepository = null)
        {
            _investimentoRepository = investimentoRepository;
            _clienteRepository = clienteRepository;
            _produtoRepository = produtoRepository;
            _transacaoRepository = transacaoRepository;
        }

        public async Task<IEnumerable<Investimento>> GetAllAsync()
        {
            return await _investimentoRepository.GetAllAsync();
        }

        public async Task<Investimento> GetByIdAsync(int id)
        {
            return await _investimentoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Investimento>> GetByClienteIdAsync(int clienteId)
        {
            var cliente = await _clienteRepository.GetByIdAsync(clienteId);
            if (cliente == null) return new List<Investimento>();

            return cliente.Investimentos;
        }

        public async Task ComprarInvestimentoAsync(int produtoId, int clienteId, int quantidade, decimal preco)
        {
            var produto = await _produtoRepository.GetByIdAsync(produtoId);
            if (produto == null) throw new Exception("Produto não encontrado.");

            var investimento = new Investimento
            {
                ClienteId = clienteId,
                ProdutoId = produtoId,
                Quantidade = quantidade,
                Preco = preco,
                DataCompra = DateTime.Now
            };

            var investimentoId = await _investimentoRepository.AddAsync(investimento);

            var transacao = new Transacao
            {
                InvestimentoId = investimentoId,
                Quantidade = quantidade,
                Preco = preco,
                Data = DateTime.UtcNow,
                Tipo = TipoTransacao.Compra
            };
            await _transacaoRepository.AddAsync(transacao);
        }

        public async Task VenderInvestimentoAsync(int id, int quantidade, decimal preco)
        {
            var investimento = await _investimentoRepository.GetByIdAsync(id);
            if (investimento == null)
            {
                throw new Exception("Investimento não encontrado");
            }

            if (investimento.Quantidade < quantidade)
            {
                throw new Exception("Quantidade insuficiente");
            }

            investimento.Quantidade -= quantidade;
            await _investimentoRepository.UpdateAsync(investimento);

            var transacao = new Transacao
            {
                InvestimentoId = id,
                Quantidade = quantidade,
                Preco = preco,
                Data = DateTime.UtcNow,
                Tipo = TipoTransacao.Venda
            };
            await _transacaoRepository.AddAsync(transacao);
        }

        public async Task<IEnumerable<Investimento>> GetByClienteCpfCnpjAsync(string cpfCnpj)
        {
            return await _investimentoRepository.GetByClienteCpfCnpjAsync(cpfCnpj);
        }
    }
}
