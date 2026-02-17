using EstateHub.Domain.Common;

namespace EstateHub.Domain.Entities;

public class AdminReviewLog : BaseEntity
{
    public Guid ListingId { get; set; }
    public Guid AdminId { get; set; }
    public string Action { get; set; } = string.Empty;  
    public string? Reason { get; set; }               
    public DateTime ReviewedAt { get; set; }

    // Navigation
    public Listing Listing { get; set; } = null!;
}