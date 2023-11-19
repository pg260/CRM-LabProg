namespace CRM.Service.Dtos.Feedback;

public class AddFeedbackDto
{
    public int ProdutoId { get; set; }
    public float Avaliacao { get; set; }
    public string? Comentarios { get; set; }
}