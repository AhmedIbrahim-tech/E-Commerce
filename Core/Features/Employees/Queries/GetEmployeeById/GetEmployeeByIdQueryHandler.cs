namespace Core.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdQueryHandler : ApiResponseHandler,
    IRequestHandler<GetEmployeeByIdQuery, ApiResponse<GetEmployeeByIdResponse>>
{
    private readonly UserManager<User> _userManager;

    public GetEmployeeByIdQueryHandler(UserManager<User> userManager) : base()
    {
        _userManager = userManager;
    }

    public async Task<ApiResponse<GetEmployeeByIdResponse>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
    {
        var employee = await _userManager.Users.FirstOrDefaultAsync(e => e.Id.Equals(request.Id));
        if (employee is null) return NotFound<GetEmployeeByIdResponse>(SharedResourcesKeys.NotFound);

        var employeeResponse = new GetEmployeeByIdResponse
        {
            Id = employee.Id,
            FullName = $"{employee.FirstName} {employee.LastName}".Trim(),
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            Gender = employee.Gender,
            Position = employee is Employee emp ? emp.Position : null,
            Salary = employee is Employee emp2 ? emp2.Salary : null,
            HireDate = employee is Employee emp3 ? emp3.HireDate : null,
            Address = employee is Employee emp4 ? emp4.Address : null
        };

        return Success(employeeResponse);
    }
}

