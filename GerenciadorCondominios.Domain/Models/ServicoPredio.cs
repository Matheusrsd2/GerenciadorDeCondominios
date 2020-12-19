using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominios.Domain.Models
{
    public class ServicoPredio
    {
        public int Id { get; set; }
        public int ServicoId { get; set; }
        public Servico Servico { get; set; }
        public DateTime DataServico { get; set; }
    }
}
