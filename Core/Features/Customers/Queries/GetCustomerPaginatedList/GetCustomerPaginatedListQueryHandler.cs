using Core.Wrappers;

namespace Core.Features.Customers.Queries.GetCustomerPaginatedList;

public class GetCustomerPaginatedListQueryHandler : ApiResponseHandler,
    IRequestHandler<GetCustomerPaginatedListQuery, PaginatedResult<GetCustomerPaginatedListResponse>>
{
    private readonly UserManager<User> _userManager;

    public GetCustomerPaginatedListQueryHandler(UserManager<User> userManager) : base()
    {
        _userManager = userManager;
    }

    public async Task<PaginatedResult<GetCustomerPaginatedListResponse>> Handle(GetCustomerPaginatedListQuery request, CancellationToken cancellationToken)
    {
        Expression<Func<Customer, GetCustomerPaginatedListResponse>> expression = c => new GetCustomerPaginatedListResponse
        {
            Id = c.Id,
            FullName = c.FirstName + " " + c.LastName,
            Email = c.Email,
            Gender = c.Gender,
            PhoneNumber = c.PhoneNumber
        };

        var customers = _userManager.Users.OfType<Customer>().AsQueryable().ApplyFiltering(request.SortBy, request.Search);
        var paginatedList = await customers.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
        paginatedList.Meta = new { Count = paginatedList.Data.Count() };
        return paginatedList;
    }
}

