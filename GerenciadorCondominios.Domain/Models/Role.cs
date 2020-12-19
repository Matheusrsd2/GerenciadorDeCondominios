using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;


namespace GerenciadorCondominios.Domain.Models
{
    public class Roles : IdentityRole<string>
    {
        public string Descricao { get; set; }
        
    }
}
