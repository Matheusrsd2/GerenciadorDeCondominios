using GerenciadorCondominios.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominios.Domain.Models
{
    public class Servico
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obrigatório informar o {0}")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Obrigatório informar o {0}")]
        public decimal Valor { get; set; }
        public StatusServico Status { get; set; }
        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
        public ICollection<ServicoPredio> ServicoPredios { get; set; }
    }
}
