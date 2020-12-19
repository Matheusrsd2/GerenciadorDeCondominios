using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominios.Domain.Models
{
    public class Mes
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Aluguel> Alugueis { get; set; }
        public ICollection<HistoricoRecursos> Historicos { get; set; }
    }
}
