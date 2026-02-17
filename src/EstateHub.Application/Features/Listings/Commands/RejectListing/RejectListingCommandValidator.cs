using FluentValidation;

namespace EstateHub.Application.Features.Listings.Commands.RejectListing;

public class RejectListingCommandValidator : AbstractValidator<RejectListingCommand>
{
    public RejectListingCommandValidator()
    {
        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("Rədd etmə səbəbi boş ola bilməz.")
            .MinimumLength(10).WithMessage("Səbəb minimum 10 simvol olmalıdır.")
            .MaximumLength(500).WithMessage("Səbəb maksimum 500 simvol olmalıdır.");
    }
}