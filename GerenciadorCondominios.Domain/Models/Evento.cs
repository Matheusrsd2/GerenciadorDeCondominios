using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominios.Domain.Models
{
    public class Evento
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O {0} é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O {0} é obrigatório")]
        public DateTime Data { get; set; }
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}
