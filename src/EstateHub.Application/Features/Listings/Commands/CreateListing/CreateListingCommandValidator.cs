using EstateHub.Domain.Enums;
using FluentValidation;

namespace EstateHub.Application.Features.Listings.Commands.CreateListing;

public class CreateListingCommandValidator : AbstractValidator<CreateListingCommand>
{
    public CreateListingCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlıq boş ola bilməz.")
            .MinimumLength(10).WithMessage("Başlıq minimum 10 simvol olmalıdır.")
            .MaximumLength(200).WithMessage("Başlıq maksimum 200 simvol olmalıdır.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Açıqlama boş ola bilməz.")
            .MinimumLength(20).WithMessage("Açıqlama minimum 20 simvol olmalıdır.")
            .MaximumLength(2000).WithMessage("Açıqlama maksimum 2000 simvol olmalıdır.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Qiymət 0-dan böyük olmalıdır.");

        RuleFor(x => x.Currency)
            .Must(x => x == "AZN" || x == "USD")
            .WithMessage("Valyuta AZN və ya USD olmalıdır.");

        RuleFor(x => x.Area)
            .GreaterThan(0).WithMessage("Sahə 0-dan böyük olmalıdır.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("Şəhər boş ola bilməz.");

        RuleFor(x => x.District)
            .NotEmpty().WithMessage("Rayon boş ola bilməz.");

        RuleFor(x => x.RentPeriod)
            .NotNull().WithMessage("Kirayə müddəti seçilməlidir.")
            .When(x => x.ListingType == ListingType.Rent);

        RuleFor(x => x.Floor)
            .NotNull().WithMessage("Mərtəbə boş ola bilməz.")
            .When(x => x.PropertyType == PropertyType.Apartment);

        RuleFor(x => x.Rooms)
            .NotNull().WithMessage("Otaq sayı boş ola bilməz.")
            .GreaterThan(0).WithMessage("Otaq sayı 0-dan böyük olmalıdır.")
            .When(x => x.PropertyType == PropertyType.Apartment ||
                       x.PropertyType == PropertyType.House);
    }
}