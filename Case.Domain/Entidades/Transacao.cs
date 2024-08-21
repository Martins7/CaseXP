using Case.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Dominio.Entidades
{
    public class Transacao
    {
        public int Id { get; set; }
        public int InvestimentoId { get; set; }
        public Investimento Investimento { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Preco { get; set; }
        public DateTime Data { get; set; }
        public TipoTransacao Tipo { get; set; }
    }
}
