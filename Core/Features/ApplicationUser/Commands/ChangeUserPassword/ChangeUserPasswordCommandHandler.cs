namespace Core.Features.ApplicationUser.Commands.ChangeUserPassword
{
    internal class ChangeUserPasswordCommandHandler(UserManager<User> userManager) : ApiResponseHandler(),
        IRequestHandler<ChangeUserPasswordCommand, ApiResponse<string>>
    {
        public async Task<ApiResponse<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id.ToString());
            if (user is null) return NotFound<string>();

            var changePasswordResult = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            if (!changePasswordResult.Succeeded)
                return BadRequest<string>(changePasswordResult.Errors.FirstOrDefault()?.Description);
            return Success<string>(SharedResourcesKeys.PasswordChangedSuccessfully);
        }
    }
}

