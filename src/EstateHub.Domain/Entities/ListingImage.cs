using EstateHub.Domain.Common;

namespace EstateHub.Domain.Entities;

public class ListingImage : BaseEntity
{
    public Guid ListingId { get; set; }
    public string Url { get; set; } = string.Empty;
    public bool IsCover { get; set; } = false;
    public int Order { get; set; }

    // Navigation
    public Listing Listing { get; set; } = null!;
}