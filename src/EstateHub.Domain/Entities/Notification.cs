using EstateHub.Domain.Common;

namespace EstateHub.Domain.Entities;

public class Notification : BaseEntity
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public string? RelatedEntityId { get; set; } 
    public string? Type { get; set; }             
}