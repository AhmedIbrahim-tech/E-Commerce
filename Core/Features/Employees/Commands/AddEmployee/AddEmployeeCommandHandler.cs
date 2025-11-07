namespace Core.Features.Employees.Commands.AddEmployee;

public class AddEmployeeCommandHandler : ApiResponseHandler,
    IRequestHandler<AddEmployeeCommand, ApiResponse<string>>
{
    private readonly UserManager<User> _userManager;

    public AddEmployeeCommandHandler(UserManager<User> userManager) : base()
    {
        _userManager = userManager;
    }

    public async Task<ApiResponse<string>> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user != null) return BadRequest<string>(SharedResourcesKeys.EmailIsExist);

        var userByUserName = await _userManager.FindByNameAsync(request.UserName);
        if (userByUserName != null) return BadRequest<string>(SharedResourcesKeys.UserNameIsExist);

        var identityUser = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            Email = request.Email,
            Gender = request.Gender,
            PhoneNumber = request.PhoneNumber,
            Position = request.Position,
            Salary = request.Salary,
            Address = request.Address
        };

        var createResult = await _userManager.CreateAsync(identityUser, request.Password);
        if (!createResult.Succeeded)
            return BadRequest<string>(createResult.Errors.FirstOrDefault()?.Description ?? SharedResourcesKeys.CreateFailed);

        //Add default role "Employee"
        var addToRoleResult = await _userManager.AddToRoleAsync(identityUser, "Employee");
        if (!addToRoleResult.Succeeded)
            return BadRequest<string>(SharedResourcesKeys.FailedToAddNewRoles);

        //Add default employee policies
        var claims = new List<Claim>
        {
            new Claim("Edit Employee", "True"),
            new Claim("Get Employee", "True")
        };
        var addDefaultClaimsResult = await _userManager.AddClaimsAsync(identityUser, claims);
        if (!addDefaultClaimsResult.Succeeded)
            return BadRequest<string>(SharedResourcesKeys.FailedToAddNewClaims);

        return Created("");
    }
}

