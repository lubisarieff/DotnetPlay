using FluentValidation;

namespace Service.User.Command;
public class CreateUserCommandValidator : AbstractValidator<CreateUserCommmand> {
    public CreateUserCommandValidator() {
        RuleFor(v => v.username).NotEmpty().MinimumLength(5);
        RuleFor(v => v.password).NotEmpty().MinimumLength(5);
    }
}