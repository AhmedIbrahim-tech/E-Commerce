using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authorization.Commands.DeleteRole;

public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
{
    private readonly IAuthorizationService _authorizationService;

    public DeleteRoleValidator(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
        ApplyValidationRoles();
    }

    public void ApplyValidationRoles()
    {
        RuleFor(c => c.RoleId)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);
    }
}

