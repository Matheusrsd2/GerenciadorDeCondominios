using GerenciadorCondominios.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorCondominios.Domain.Models
{
    public class HistoricoRecursos
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public Tipo Tipos { get; set; }
        public int Dia { get; set; }
        public int MesId { get; set; }
        public virtual Mes Mes { get; set; }

        public int Ano { get; set; }
    }
}
