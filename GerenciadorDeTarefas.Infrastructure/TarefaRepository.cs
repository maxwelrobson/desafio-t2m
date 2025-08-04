using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciadorDeTarefas.Domain;
using Microsoft.Extensions.Configuration;


namespace GerenciadorDeTarefas.Infrastructure
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly string _connectionString;

        public TarefaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
    }
}
