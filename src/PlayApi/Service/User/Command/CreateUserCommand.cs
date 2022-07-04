using ErrorOr;
using MediatR;

namespace Service.User.Command;

public record CreateUserCommmand(string? username, string? password) : IRequest<ErrorOr<User>>;

public class CreateUserCommmandHandler : IRequestHandler<CreateUserCommmand, ErrorOr<User>>
{
    public readonly IList<User> _users = new List<User>();

    public Task<ErrorOr<User>> Handle(CreateUserCommmand request, CancellationToken cancellationToken)
    {
        (string? username, string? password) = request;
        ErrorOr<User> user = new User(username, password);
        if (user.Value is null)
        {
            Error.Failure("Cannot createuser");
            return Task.FromResult(user);
        }
        _users.Add(user.Value);
        return Task.FromResult(user);
    }
}

public record User(string? Username, string? Password);