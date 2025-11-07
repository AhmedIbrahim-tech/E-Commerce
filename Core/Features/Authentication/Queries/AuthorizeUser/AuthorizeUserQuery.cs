namespace Core.Features.Authentication.AuthorizeUser;

public record AuthorizeUserQuery(string AccessToken) : IRequest<ApiResponse<string>>;

