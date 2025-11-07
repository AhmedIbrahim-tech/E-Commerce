namespace Core.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandler : ApiResponseHandler,
    IRequestHandler<DeleteCustomerCommand, ApiResponse<string>>
{
    private readonly UserManager<User> _userManager;

    public DeleteCustomerCommandHandler(UserManager<User> userManager) : base()
    {
        _userManager = userManager;
    }

    public async Task<ApiResponse<string>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _userManager.FindByIdAsync(request.Id.ToString());
        if (customer is null) return NotFound<string>();

        var deleteResult = await _userManager.DeleteAsync(customer);
        if (!deleteResult.Succeeded)
            return BadRequest<string>(SharedResourcesKeys.DeleteFailed);
        return Deleted<string>();
    }
}

