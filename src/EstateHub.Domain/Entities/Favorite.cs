using EstateHub.Domain.Common;

namespace EstateHub.Domain.Entities;

public class Favorite : BaseEntity
{
    public Guid ListingId { get; set; }
    public Guid UserId { get; set; }

    // Navigation
    public Listing Listing { get; set; } = null!;
}