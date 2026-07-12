using Application.Extensions;
using Application.Features.Users.Queries;
using FluentValidation;

namespace Application.Validators.UserValidators
{
    public class GetUserByPhoneNumberQueryValidator : AbstractValidator<GetUserByPhoneNumberQuery>
    {
        public GetUserByPhoneNumberQueryValidator()
        {
            RuleFor(r => r.phoneNumber).TurkishPhoneNumber();
        }
    }
}
