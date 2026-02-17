using EstateHub.Domain.Common;

namespace EstateHub.Domain.Entities;

public class Review : BaseEntity
{
    public Guid ListingId { get; set; }
    public Guid UserId { get; set; }
    //1-5 rating scale
    public int Rating { get; set; }      
    public string Comment { get; set; } = string.Empty;

    // Navigation
    public Listing Listing { get; set; } = null!;
}