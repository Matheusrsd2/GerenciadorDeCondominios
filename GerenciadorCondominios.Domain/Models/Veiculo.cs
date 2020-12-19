using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominios.Domain.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        [Required(ErrorMessage= "Obrigatório informar o {0}")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Obrigatório informar a {0}")]
        public string Marca { get; set; }
        public string Placa { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
