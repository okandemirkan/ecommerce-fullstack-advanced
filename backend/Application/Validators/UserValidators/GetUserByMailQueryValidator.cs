using Application.Extensions;
using Application.Features.Users.Queries;
using FluentValidation;

namespace Application.Validators.UserValidators
{
    public class GetUserByMailQueryValidator : AbstractValidator<GetUserByMailQuery>
    {
        public GetUserByMailQueryValidator()
        {
            RuleFor(r => r.email).EMailValidation();
        }
    }
}
