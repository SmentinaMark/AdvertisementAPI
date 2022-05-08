using adAPI.Contracts.Requests;
using adAPI.Models;
using FluentValidation;

namespace adAPI.Validation
{
    public class AdvertisementValidator : AbstractValidator<CreateAdvertisement>
    {
        public AdvertisementValidator()
        {

            RuleFor(ad => ad.Title).NotEmpty().WithMessage("Must be filled with a non-empty value")
                                   .MinimumLength(1).WithMessage("The length of the Title should be from 1 to 200 characters.")
                                   .MaximumLength(200).WithMessage("The length of the Title should be from 1 to 200 characters.");
            RuleFor(ad => ad.Description).NotEmpty().WithMessage("Must be filled with a non-empty value")
                                        .MinimumLength(1).WithMessage("The length of the Description should be from 1 to 1000 characters.")
                                        .MaximumLength(1000).WithMessage("The length of the Description should be from 1 to 1000 characters.");
            RuleFor(ad => ad._Images.Length).LessThanOrEqualTo(3).WithMessage("The number of images should be less or equal 3.");
        }
    }
}
