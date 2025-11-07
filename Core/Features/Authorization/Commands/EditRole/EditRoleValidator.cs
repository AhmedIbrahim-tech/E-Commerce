using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authorization.Commands.EditRole;

public class EditRoleValidator : AbstractValidator<EditRoleCommand>
{
    private readonly IAuthorizationService _authorizationService;

    public EditRoleValidator(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
        ApplyValidationRoles();
        ApplyCustomValidationRoles();
    }

    public void ApplyValidationRoles()
    {
        RuleFor(c => c.RoleId)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required);

        RuleFor(c => c.RoleName)
            .NotEmpty().WithMessage(SharedResourcesKeys.NotEmpty)
            .NotNull().WithMessage(SharedResourcesKeys.Required)
            .MaximumLength(100).WithMessage(SharedResourcesKeys.MaxLengthIs100);
    }

    public void ApplyCustomValidationRoles()
    {
        RuleFor(c => c.RoleName)
            .MustAsync(async (model, name, cancellation) => !await _authorizationService.IsRoleExistExcludeSelf(name, model.RoleId))
            .WithMessage(SharedResourcesKeys.IsExist);
    }
}

