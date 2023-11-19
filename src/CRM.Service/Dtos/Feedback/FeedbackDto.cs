using CRM.Domain.Entities;
using CRM.Service.Dtos.ComprasDto;

namespace CRM.Service.Dtos.Feedback;

public class FeedbackDto
{
    public int Id { get; set; }
    public int CompraId { get; set; }
    public float Avaliacao { get; set; }
    public string? Comentarios { get; set; }

    public ComprasFeedbackDto Compra { get; set; } = null!;
}