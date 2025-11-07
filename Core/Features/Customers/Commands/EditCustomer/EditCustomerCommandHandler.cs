using Core.Wrappers;

namespace Core.Features.Customers.Commands.EditCustomer;

public class EditCustomerCommandHandler : ApiResponseHandler,
    IRequestHandler<EditCustomerCommand, ApiResponse<string>>
{
    private readonly UserManager<User> _userManager;

    public EditCustomerCommandHandler(UserManager<User> userManager) : base()
    {
        _userManager = userManager;
    }

    public async Task<ApiResponse<string>> Handle(EditCustomerCommand request, CancellationToken cancellationToken)
    {
        var oldCustomer = await _userManager.FindByIdAsync(request.Id.ToString());
        if (oldCustomer is null) return NotFound<string>();

        var isUserNameDuplicate = await _userManager.UserNameExistsAsync(request.UserName!, request.Id);
        if (isUserNameDuplicate)
            return BadRequest<string>(SharedResourcesKeys.UserNameIsExist);

        var isEmailDuplicate = await _userManager.EmailExistsAsync(request.Email!, request.Id);
        if (isEmailDuplicate)
            return BadRequest<string>(SharedResourcesKeys.EmailIsExist);

        // Manually update properties
        oldCustomer.FirstName = request.FirstName;
        oldCustomer.LastName = request.LastName;
        oldCustomer.UserName = request.UserName;
        oldCustomer.Email = request.Email;
        oldCustomer.Gender = request.Gender;
        oldCustomer.PhoneNumber = request.PhoneNumber;

        var updateResult = await _userManager.UpdateAsync(oldCustomer);

        if (!updateResult.Succeeded)
            return BadRequest<string>(SharedResourcesKeys.UpdateFailed);
        return Edit("");
    }
}

