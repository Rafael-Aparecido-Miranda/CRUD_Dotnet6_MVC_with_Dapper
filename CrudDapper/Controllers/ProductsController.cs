using CrudDapper.Models;
using CrudDapper.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;

namespace CrudDapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _repository.SearchProductsAsync();


            return products.Any() ? Ok(products) : NoContent();
        }
        
        [HttpGet("id")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _repository.SearchProductAsync(id);

            return product != null ? Ok(product) : NotFound("Produto não cadastrado");
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductRequest request) 
        {
            if (string.IsNullOrEmpty(request.Nome))
            {
                return BadRequest("Informações inválidas");
            }
            var addon = await _repository.AddAsync(request);

            return addon ? Ok("Adicionado com sucesso") : BadRequest("Erro ao adicionar");
        }

        [HttpPut("id")]
        public async Task<IActionResult> Put(ProductRequest request, int id)
        {
            var product = await _repository.SearchProductAsync(id);

            if (id < 0) return BadRequest("Código inválido");

            if (product == null) NotFound("Produto não listado");

            if (string.IsNullOrEmpty(request.Nome)) request.Nome = product.Nome;
            if (string.IsNullOrEmpty(request.Tamanho)) request.Tamanho = product.Tamanho;
            if (string.IsNullOrEmpty(request.Fotos)) request.Fotos = product.Fotos;
            

            var update = await _repository.UpdateAsync(request, id);

            return update ? Ok("Atualizado com sucesso") : BadRequest("Erro ao atualizar");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(ProductRequest request, int id) 
        {
            var product = await _repository.SearchProductAsync(id);

            if (id < 0) return BadRequest("Código inválido");

            if (product == null) return NotFound("Produto não listado");

            var deleted = await _repository.DeleteAsync(id);

            return deleted ? Ok("Deletado com sucesso") : BadRequest("Erro ao deletar");

        }
    }
}
