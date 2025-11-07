using Service.ServicesHandlers.Interfaces;

namespace Core.Features.Authentication.SignIn;

public class SignInCommandHandler : ApiResponseHandler,
    IRequestHandler<SignInCommand, ApiResponse<JwtAuthResponse>>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IAuthenticationService _authenticationService;

    public SignInCommandHandler(UserManager<User> userManager,
        SignInManager<User> signInManager,
        IAuthenticationService authenticationService) : base()
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _authenticationService = authenticationService;
    }

    public async Task<ApiResponse<JwtAuthResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user is null) return BadRequest<JwtAuthResponse>(SharedResourcesKeys.UserNameIsNotExist);

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!user.EmailConfirmed) return BadRequest<JwtAuthResponse>(SharedResourcesKeys.EmailIsNotConfirmed);
        if (!signInResult.Succeeded) return BadRequest<JwtAuthResponse>(SharedResourcesKeys.InvalidPassword);

        var result = await _authenticationService.GetJWTTokenAsync(user);
        return Success(result);
    }
}

