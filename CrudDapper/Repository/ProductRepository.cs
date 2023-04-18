using CrudDapper.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CrudDapper.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        private readonly string connectionString;

        public ProductRepository(IConfiguration configuration) 
        {
            this._configuration = configuration;
            connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public async Task<IEnumerable<ProductResponse>> SearchProductsAsync()
        {
            using var con = new SqlConnection(connectionString);
 
            string sql =
                @"
                    SELECT 
                    tb_produto.id Id,
                    tb_produto.nome Nome,
                    tb_marcas.nome Marcas,
                    tb_produto.tamanho Tamanho,
                    tb_produto.fotos Foto
                    FROM tb_produto
                    JOIN tb_marcas ON tb_marcas.id = tb_produto.marcas_id;
                ";
                return await con.QueryAsync<ProductResponse>(sql);
            
            
        }

        public async Task<ProductResponse> SearchProductAsync(int id)
        {
            string sql =
                @"
                SELECT 
                    tb_produto.id Id,
                    tb_produto.nome Nome,
                    tb_marcas.nome Marcas,
                    tb_produto.tamanho Tamanho,
                    tb_produto.fotos Foto
                FROM tb_produto
                JOIN tb_marcas ON tb_produto.marcas_id = tb_marcas.id
                WHERE tb_produto.id = @Id";
            using var con = new SqlConnection(connectionString);
            return await con.QueryFirstOrDefaultAsync<ProductResponse>(sql, new {Id = id}); 
            // QueryFirstOrDefaultAsync retorna um null se for mais de um resultado ou umid que nao existe ao inves de um erro

        }


        public async Task<bool> AddAsync(ProductRequest request)
        {
            string sql = @"
                INSERT INTO tb_produto(nome, tamanho, marcas_id, fotos)
                VALUES (@Nome,@Tamanho, @MarcasId, @Fotos)";
            using var con = new SqlConnection(connectionString);

            return await con.ExecuteAsync(sql,request) > 0;
        }
        public async Task<bool> UpdateAsync(ProductRequest request, int id)
        {
            //MODIFICAR MUDANÇA DA MARCA DO PRODUTO ERRO DE CONVERSÃO DE STRING PARA INT ESTÁ PEGANDO O VALOR DA COLUNA E TENTANDO CONVERTER PARA NUMERO
            //string marcaSql = "SELECT * FROM tb_marcas WHERE id = @MarcasId";
            string sql = @"
                UPDATE tb_produto SET 
                nome = @Nome, tamanho = @Tamanho, fotos = @Fotos
                WHERE tb_produto.id = @Id";

            
            var parameters = new DynamicParameters();
            parameters.Add("Nome", request.Nome);
            parameters.Add("Tamanho", request.Tamanho);
            parameters.Add("Fotos", request.Fotos);
            parameters.Add("Id", id);

            using var con = new SqlConnection(connectionString);
            
            return await con.ExecuteAsync(sql, parameters) > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            string sql = @"
                DELETE FROM tb_produto
                WHERE id = @Id";
            using var con = new SqlConnection( connectionString);
            return await con.ExecuteAsync(sql, new {Id = id}) > 0;
        }
    }
}
