using ErrorOr;
using FluentValidation;
using MediatR;

namespace PlayApi.Pipeline;

public class ValidationBehavior<TRequest, TResult> : IPipelineBehavior<TRequest,TResult>
    where TRequest : IRequest<TResult>
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator)
    {
        _validator = validator;
    }

    public async Task<ErrorOr<TResult>> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<ErrorOr<TResult>> next)
    {
        if (_validator == null)
        {
            return await next();
        }

        var validationResult = _validator.Validate(request);

        if (validationResult.Errors.Any())
        {
            return validationResult.Errors
               .ConvertAll(validationFailure => Error.Validation(
                   code: validationFailure.PropertyName,
                   description: validationFailure.ErrorMessage));
        }

        return await next();
    }

    public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResult> next)
    {
        return await next();
    }
}