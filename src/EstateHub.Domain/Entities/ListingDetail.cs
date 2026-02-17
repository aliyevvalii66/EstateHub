using EstateHub.Domain.Common;

namespace EstateHub.Domain.Entities;

public class ListingDetail : BaseEntity
{
    public Guid ListingId { get; set; }
    //m2
    public double Area { get; set; }       
    public int? Rooms { get; set; }
    public int? Floor { get; set; }
    public int? TotalFloors { get; set; }
    public bool HasGarage { get; set; }
    public bool HasGarden { get; set; }
    public bool HasPool { get; set; }
    public int? BuildYear { get; set; }

    public Listing Listing { get; set; } = null!;
}