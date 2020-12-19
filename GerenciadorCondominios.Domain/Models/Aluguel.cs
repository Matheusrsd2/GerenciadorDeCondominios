using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominios.Domain.Models
{
    public class Aluguel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório informar o {0}")]
        [Range(0, int.MaxValue, ErrorMessage = "Valor Invalido")]
        public decimal Valor { get; set; }
        [Display(Name = "Mes")]
        public int MesId { get; set; }
        public Mes Mes { get; set; }
        public int Ano { get; set; }
        public virtual ICollection<Pagamento> Pagamentos { get; set; }
    }
}
