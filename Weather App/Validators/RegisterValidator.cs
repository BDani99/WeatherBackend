using FluentValidation;
using Secret_Sharing_Platform.Dtos;
using Weather_App.Helpers;
using Weather_App.Repository;

namespace Weather_App.Validator
{
    public class RegisterValidator : AbstractValidator<RegisterDto>
    {
        public RegisterValidator(IUserRepository _userRepository)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ErrorConstans.NAME_IS_REQUIRED);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(ErrorConstans.EMAIL_IS_REQUIRED)
                .EmailAddress().WithMessage(ErrorConstans.INVALID_EMAIL_FORMAT);
            RuleFor(x => x.Email)
                .Must(_userRepository.AlreadyUserWithThisEmail)
                .WithMessage(ErrorConstans.THERE_IS_USER_WITH_THIS_EMAIL);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ErrorConstans.PASSWORD_IS_REQUIRED)
                .MinimumLength(8).WithMessage(ErrorConstans.PASSWORD_IS_TOO_SHORT)
                .Matches("[A-Z]").WithMessage(ErrorConstans.PASSWORD_MUST_HAVE_UPPERCASE_LETTER)
                .Matches("[a-z]").WithMessage(ErrorConstans.PASSWORD_MUST_HAVE_LOWERCASE_LETTER)
                .Matches("[0-9]").WithMessage(ErrorConstans.PASSWORD_MUST_HAVE_NUMBER);
        }
    }
}
