using Application.Extensions;
using Application.Features.Users.Commands;
using FluentValidation;

namespace Application.Validators.UserValidators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(r => r.UpdateUserDTO.Email).EMailValidation();
            RuleFor(c => c.UpdateUserDTO.PhoneNumber).TurkishPhoneNumber();

            RuleFor(c => c.UpdateUserDTO.UserName)
              .MinimumLength(3).WithMessage("User Name must be at least 3 characters.")
              .MaximumLength(45).WithMessage("User Name must be at most 45 characters.");
        }
    }
}
