using Core.Wrappers;

namespace Core.Features.Employees.Commands.EditEmployee;

public class EditEmployeeCommandHandler : ApiResponseHandler,
    IRequestHandler<EditEmployeeCommand, ApiResponse<string>>
{
    private readonly UserManager<User> _userManager;

    public EditEmployeeCommandHandler(UserManager<User> userManager) : base()
    {
        _userManager = userManager;
    }

    public async Task<ApiResponse<string>> Handle(EditEmployeeCommand request, CancellationToken cancellationToken)
    {
        var oldEmployee = await _userManager.FindByIdAsync(request.Id.ToString());
        if (oldEmployee is null) return NotFound<string>();

        var isUserNameDuplicate = await _userManager.UserNameExistsAsync(request.UserName, request.Id);
        if (isUserNameDuplicate)
            return BadRequest<string>(SharedResourcesKeys.UserNameIsExist);

        var isEmailDuplicate = await _userManager.EmailExistsAsync(request.Email, request.Id);
        if (isEmailDuplicate)
            return BadRequest<string>(SharedResourcesKeys.EmailIsExist);

        // Manually update properties
        oldEmployee.FirstName = request.FirstName;
        oldEmployee.LastName = request.LastName;
        oldEmployee.UserName = request.UserName;
        oldEmployee.Email = request.Email;
        oldEmployee.Gender = request.Gender;
        oldEmployee.PhoneNumber = request.PhoneNumber;

        if (oldEmployee is Employee employee)
        {
            employee.Position = request.Position;
            employee.Salary = request.Salary;
            employee.Address = request.Address;
        }

        var updateResult = await _userManager.UpdateAsync(oldEmployee);

        if (!updateResult.Succeeded)
            return BadRequest<string>(SharedResourcesKeys.UpdateFailed);
        return Edit("");
    }
}

