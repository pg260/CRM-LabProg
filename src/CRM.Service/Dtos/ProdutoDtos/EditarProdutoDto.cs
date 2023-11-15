namespace CRM.Service.Dtos.ProdutoDtos
{
    public class EditarProdutoDto
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public float? Valor { get; set; }
        public string? Descricao { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
    }
}