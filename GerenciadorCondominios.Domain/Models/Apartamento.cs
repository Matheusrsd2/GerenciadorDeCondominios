using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominios.Domain.Models
{
    public class Apartamento
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O {0} é obrigatório")]
        public int Numero { get; set; }
        [Required(ErrorMessage = "O {0} é obrigatório")]
        public int Andar { get; set; }
        public string Foto { get; set; }
        public string MoradorId { get; set; }
        public virtual Usuario Morador { get; set; }

        public string ProprietarioId { get; set; }
        public virtual Usuario Proprietario { get; set; }

    }
}
