namespace Core.Features.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery(Guid Id) : IRequest<ApiResponse<GetCustomerByIdResponse>>;

