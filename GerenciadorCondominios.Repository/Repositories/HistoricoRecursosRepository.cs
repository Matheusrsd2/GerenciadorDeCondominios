using GerenciadorCondominios.Domain.Models;
using GerenciadorCondominios.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerenciadorCondominios.Repository.Repositories
{
    public class HistoricoRecursosRepository : Repository<HistoricoRecursos>, IHistoricoRecursosRepository
    {
        public HistoricoRecursosRepository(Context contexto) : base(contexto)
        {
            
        }
    }
}
