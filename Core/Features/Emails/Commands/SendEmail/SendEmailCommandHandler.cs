namespace Core.Features.Emails.Commands.SendEmail;

public class SendEmailCommandHandler : ApiResponseHandler,
    IRequestHandler<SendEmailCommand, ApiResponse<string>>
{
    private readonly IEmailsService _emailsService;

    public SendEmailCommandHandler(IEmailsService emailsService) : base()
    {
        _emailsService = emailsService;
    }

    public async Task<ApiResponse<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var response = await _emailsService.SendEmailAsync(request.Email, request.ReturnUrl, request.EmailType);
        if (response == "Success")
            return Success("");
        return BadRequest<string>("SendEmailFailed");
    }
}

