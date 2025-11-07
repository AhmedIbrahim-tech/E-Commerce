using Core.Wrappers;

namespace Core.Features.Employees.Queries.GetEmployeePaginatedList;

public class GetEmployeePaginatedListQueryHandler : ApiResponseHandler,
    IRequestHandler<GetEmployeePaginatedListQuery, PaginatedResult<GetEmployeePaginatedListResponse>>
{
    private readonly UserManager<User> _userManager;

    public GetEmployeePaginatedListQueryHandler(UserManager<User> userManager) : base()
    {
        _userManager = userManager;
    }

    public async Task<PaginatedResult<GetEmployeePaginatedListResponse>> Handle(GetEmployeePaginatedListQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Employee, GetEmployeePaginatedListResponse>> expression = c => new GetEmployeePaginatedListResponse
        {
            Id = c.Id,
            FullName = c.FirstName + " " + c.LastName,
            Email = c.Email,
            Gender = c.Gender,
            PhoneNumber = c.PhoneNumber,
            Position = c.Position,
            Salary = c.Salary,
            HireDate = c.HireDate,
            Address = c.Address
        };

        var employees = _userManager.Users.OfType<Employee>().AsQueryable().ApplyFiltering(request.SortBy, request.Search);
        var paginatedList = await employees.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
        paginatedList.Meta = new { Count = paginatedList.Data.Count() };
        return paginatedList;
    }
}

