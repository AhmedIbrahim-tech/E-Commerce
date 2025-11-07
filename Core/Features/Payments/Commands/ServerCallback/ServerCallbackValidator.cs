using System.Text.Json;

namespace Core.Features.Payments.Commands.ServerCallback;

public class ServerCallbackValidator : AbstractValidator<ServerCallbackCommand>
{
    public ServerCallbackValidator()
    {
        ApplyValidationRules();
    }

    public void ApplyValidationRules()
    {
        RuleFor(c => c.Payload.ValueKind)
            .NotEqual(JsonValueKind.Undefined).WithMessage(SharedResourcesKeys.InvalidPayload)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);

        RuleFor(c => c.Hmac)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);
    }
}

