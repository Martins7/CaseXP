using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Case.Dominio.DTOs
{
    public class CompraVendaDto
    {
        public int InvestimentoId { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public int ClienteId { get; set; }
    }
}
