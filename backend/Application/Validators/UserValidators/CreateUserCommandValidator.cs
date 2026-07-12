using Application.Extensions;
using Application.Features.Users.Commands;
using FluentValidation;
namespace Application.Validators.UserValidators
{
    public class CreateUserCommandValidator : AbstractValidator<RegisterCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.User.EMail).EMailValidation();
            RuleFor(u => u.User.PhoneNumber).TurkishPhoneNumber();

            RuleFor(u => u.User.UserName)
               .MinimumLength(3).WithMessage("User Name must be at least 3 characters.")
              .MaximumLength(45).WithMessage("User Name must be at most 45 characters.");

            RuleFor(u => u.User.Password)
                .MinimumLength(7).WithMessage("Password must be at least 7 charachters."); //more validations will be added.
        }
    }
}
