namespace CrudDapper.Models
{
    public class ProductRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tamanho { get; set; }
        public int MarcasId { get; set; }
        public string Fotos { get; set; }
    }
}
