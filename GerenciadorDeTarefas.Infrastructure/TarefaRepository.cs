using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using GerenciadorDeTarefas.Domain;
using Microsoft.Extensions.Configuration;
using Npgsql;


namespace GerenciadorDeTarefas.Infrastructure
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly string _connectionString;

        //Configuração do repositório para acessar o banco de dados
        public TarefaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        //Método que retorna uma conexão aberta com o banco
        private IDbConnection CreateConnection()
        { 
            return new NpgsqlConnection(_connectionString);
        }

        public async Task AdicionarAsync(Tarefa tarefa)
        {
            var sql = @"
            INSERT INTO ""Tarefas"" (""Id"", ""Titulo"", ""Descricao"", ""Status"", ""DataCriacao"", ""DataConclusao"")
            VALUES (@Id, @Titulo, @Descricao, @Status, @DataCriacao, @DataConclusao)";

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(sql, tarefa);
            }
        }

        public async Task<Tarefa?> ObterPorIdAsync(Guid id)
        {
            var sql = @"SELECT * FROM ""Tarefas"" WHERE ""Id"" = @Id";
            using (var connection = CreateConnection())
            {
                // QuerySingleOrDefaultAsync retorna um único elemento ou nulo se não encontrar
                return await connection.QueryFirstOrDefaultAsync<Tarefa>(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<Tarefa>> ObterTodasAsync()
        {
            var sql = @"SELECT * FROM ""Tarefas"" ORDER BY ""DataCriacao"" DESC";
            using (var connection = CreateConnection())
            {
                // QueryAsync é para consultas que retornam múltiplos registros
                return await connection.QueryAsync<Tarefa>(sql);
            }
        }

        public async Task AtualizarAsync(Tarefa tarefa)
        {
            var sql = @"
            UPDATE ""Tarefas""
            SET ""Titulo"" = @Titulo,
                ""Descricao"" = @Descricao,
                ""Status"" = @Status,
                ""DataConclusao"" = @DataConclusao
            WHERE ""Id"" = @Id";

            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(sql, tarefa);
            }
        }

        public async Task RemoverAsync(Guid id)
        {
            var sql = @"DELETE FROM ""Tarefas"" WHERE ""Id"" = @Id";
            using (var connection = CreateConnection())
            {
                await connection.ExecuteAsync(sql, new { Id = id });
            }
        }


    }
}
