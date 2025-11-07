using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authentication.RefreshToken;

public class RefreshTokenCommandHandler : ApiResponseHandler,
    IRequestHandler<RefreshTokenCommand, ApiResponse<JwtAuthResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly IAuthenticationService _authenticationService;

    public RefreshTokenCommandHandler(UserManager<User> userManager,
        IAuthenticationService authenticationService) : base()
    {
        _userManager = userManager;
        _authenticationService = authenticationService;
    }

    public async Task<ApiResponse<JwtAuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var jwtToken = _authenticationService.ReadJwtToken(request.AccessToken);
        var userIdAndExpireDate = await _authenticationService.ValidateDetails(jwtToken, request.AccessToken, request.RefreshToken);
        switch (userIdAndExpireDate)
        {
            case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResponse>(SharedResourcesKeys.AlgorithmIsWrong);
            case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthResponse>(SharedResourcesKeys.TokenIsNotExpired);
            case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthResponse>(SharedResourcesKeys.RefreshTokenIsNotFound);
            case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthResponse>(SharedResourcesKeys.RefreshTokenIsExpired);
        }
        var (userId, expiryDate) = userIdAndExpireDate;
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound<JwtAuthResponse>();
        }
        var result = await _authenticationService.GetRefreshTokenAsync(user, jwtToken, expiryDate, request.RefreshToken);
        return Success(result);
    }
}

