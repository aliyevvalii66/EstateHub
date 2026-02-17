using EstateHub.Domain.Common;

namespace EstateHub.Domain.Entities;

public class Booking : BaseEntity
{
    public Guid ListingId { get; set; }
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; } = "Pending"; 

    // Navigation
    public Listing Listing { get; set; } = null!;
}