using CidadesAuth.AppConfig;
using CidadesAuth.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CidadesAuth.Repository
{
    //Classe que se conecta diretamente ao banco de dados.
    //Recebe as requisições do controlador e CidadesController e
    //executa o sql necessario para as operacoes CRUD.
    public class CidadesRepository : ICidadesRepository
    {
        private readonly ConnectionString connectionString;

        public CidadesRepository(ConnectionString connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task<IEnumerable<Cidade>> GetAll()
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString.Value))
            {
                string query = "SELECT * FROM cidades";
                dbConnection.Open();
                return await dbConnection.QueryAsync<Cidade>(query);
            }
        }

        public async Task<Cidade> GetOne(int codigo)
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString.Value))
            {
                string query = @"SELECT * FROM cidades WHERE codigo = @Codigo";
                dbConnection.Open();
                return await dbConnection.QueryFirstOrDefaultAsync<Cidade>(query, new { Codigo = codigo });
            }
        }

        //Este metodo retorna a chave primaria do objeto inserido
        public async Task<int> Add(Cidade cidade)
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString.Value))
            {
                string query = @"INSERT INTO cidades (nome, uf) OUTPUT INSERTED.codigo VALUES (@Nome, @UF)";
                dbConnection.Open();
                return await dbConnection.ExecuteScalarAsync<int>(query, cidade);
            }
        }

        public async Task<int> Update(Cidade cidade)
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString.Value))
            {
                string query = @"UPDATE cidades SET nome = @Nome, uf = @UF WHERE codigo = @Codigo";
                dbConnection.Open();
                return await dbConnection.ExecuteAsync(query, cidade);
            }
        }

        public async Task<int> Delete(int codigo)
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString.Value))
            {
                string query = @"DELETE FROM cidades WHERE codigo = @Codigo";
                dbConnection.Open();
                return await dbConnection.ExecuteAsync(query, new { Codigo = codigo });
            }
        }

        public async Task<IEnumerable<CidadesPorUf>> GetCidadesPorUF()
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString.Value))
            {
                //Chama o stored procedure no banco de dados
                string query = "cidades_por_uf";
                dbConnection.Open();
                return await dbConnection.QueryAsync<CidadesPorUf>(query);
            }
        }

        public async Task<Usuario> GetUsuario(Usuario usuario)
        {
            using (IDbConnection dbConnection = new SqlConnection(connectionString.Value))
            {
                string query = @"SELECT * FROM usuarios WHERE nome = @Nome AND senha = @Senha";
                dbConnection.Open();
                return await dbConnection.QueryFirstOrDefaultAsync<Usuario>(query, usuario);
            }
        }
    }
}
