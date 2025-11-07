namespace Core.Features.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandler : ApiResponseHandler,
    IRequestHandler<GetCustomerByIdQuery, ApiResponse<GetCustomerByIdResponse>>
{
    private readonly UserManager<User> _userManager;

    public GetCustomerByIdQueryHandler(UserManager<User> userManager) : base()
    {
        _userManager = userManager;
    }

    public async Task<ApiResponse<GetCustomerByIdResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _userManager.Users.FirstOrDefaultAsync(e => e.Id.Equals(request.Id));
        if (customer is null) return NotFound<GetCustomerByIdResponse>(SharedResourcesKeys.NotFound);

        var customerResponse = new GetCustomerByIdResponse
        {
            Id = customer.Id,
            FullName = $"{customer.FirstName} {customer.LastName}".Trim(),
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Gender = customer.Gender
        };

        return Success(customerResponse);
    }
}

