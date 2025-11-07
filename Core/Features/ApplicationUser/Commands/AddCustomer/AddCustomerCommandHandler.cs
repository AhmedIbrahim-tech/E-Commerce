namespace Core.Features.ApplicationUser.Commands.AddCustomer;

internal class AddCustomerCommandHandler(IApplicationUserService applicationUserService) : ApiResponseHandler(),
    IRequestHandler<AddCustomerCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var identityUser = new Customer
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
            Gender = request.Gender,
            PhoneNumber = request.PhoneNumber
        };
        var createResult = await applicationUserService.AddUserAsync(identityUser, request.Password);
        return createResult switch
        {
            "EmailIsExist" => BadRequest<string>(SharedResourcesKeys.EmailIsExist),
            "UserNameIsExist" => BadRequest<string>(SharedResourcesKeys.UserNameIsExist),
            "FailedToAddNewRoles" => BadRequest<string>(SharedResourcesKeys.FailedToAddNewRoles),
            "FailedToAddNewClaims" => BadRequest<string>(SharedResourcesKeys.FailedToAddNewClaims),
            "SendEmailFailed" => BadRequest<string>(SharedResourcesKeys.SendEmailFailed),
            "Failed" => BadRequest<string>(SharedResourcesKeys.TryToRegisterAgain),
            "Success" => Created(""),
            _ => BadRequest<string>(createResult)
        };
    }
}

