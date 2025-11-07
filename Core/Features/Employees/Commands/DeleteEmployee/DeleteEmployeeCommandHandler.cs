namespace Core.Features.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeCommandHandler : ApiResponseHandler,
    IRequestHandler<DeleteEmployeeCommand, ApiResponse<string>>
{
    private readonly UserManager<User> _userManager;

    public DeleteEmployeeCommandHandler(UserManager<User> userManager) : base()
    {
        _userManager = userManager;
    }

    public async Task<ApiResponse<string>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _userManager.FindByIdAsync(request.Id.ToString());
        if (employee is null) return NotFound<string>();

        var deleteResult = await _userManager.DeleteAsync(employee);
        if (!deleteResult.Succeeded)
            return BadRequest<string>(SharedResourcesKeys.DeleteFailed);
        return Deleted<string>();
    }
}

