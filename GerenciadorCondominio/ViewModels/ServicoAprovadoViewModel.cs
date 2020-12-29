using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorCondominio.ViewModels
{
    public class ServicoAprovadoViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Data de execução")]
        public DateTime Data { get; set; }
    }
}