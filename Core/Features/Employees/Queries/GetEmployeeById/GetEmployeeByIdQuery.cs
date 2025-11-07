namespace Core.Features.Employees.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery(Guid Id) : IRequest<ApiResponse<GetEmployeeByIdResponse>>;

