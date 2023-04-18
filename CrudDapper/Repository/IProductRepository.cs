using CrudDapper.Models;

namespace CrudDapper.Repository
{
    public interface IProductRepository
    {
        //FUNÇÃO ASYNC É UMA PROMISE RETORNA UM VALOR CASO SE RESOLVA ABRIR UM TRY/CATCH
        Task<IEnumerable<ProductResponse>> SearchProductsAsync(); // Faz a busca geral
        Task<ProductResponse> SearchProductAsync(int id); // Faz a busca por id
        Task<bool> AddAsync(ProductRequest request); // Adiciona um novo produto ao catalogo
        Task<bool> UpdateAsync(ProductRequest request, int id); // Pega a requisição e o id que irá ser atualizado
        Task<bool> DeleteAsync(int id); // Deleta buscando o id

    }
}
