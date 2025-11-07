using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authorization.Commands.AddRole;

public class AddRoleValidator : AbstractValidator<AddRoleCommand>
{
    private readonly IAuthorizationService _authorizationService;

    public AddRoleValidator(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
        ApplyValidationRoles();
        ApplyCustomValidationRoles();
    }

    public void ApplyValidationRoles()
    {
        RuleFor(c => c.RoleName)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthIs100);
    }

    public void ApplyCustomValidationRoles()
    {
        RuleFor(c => c.RoleName)
            .MustAsync(async (name, cancellation) => !await _authorizationService.IsRoleExistByName(name))
            .WithMessage(SharedResourcesKeys.IsExist);
    }
}

