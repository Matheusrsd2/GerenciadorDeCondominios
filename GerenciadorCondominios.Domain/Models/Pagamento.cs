using GerenciadorCondominios.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominios.Domain.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public int AluguelId { get; set; }
        public Aluguel Aluguel { get; set; }
        public DateTime? DataPagamento { get; set; }
        public StatusPagamento StatusPagamento { get; set; }
    }
}
