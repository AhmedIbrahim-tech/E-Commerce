namespace Core.Features.Payments.Queries.PaymobCallback;

public class PaymobCallbackQueryHandler : ApiResponseHandler,
    IRequestHandler<PaymobCallbackQuery, PaymobCallbackResponse>
{
    private readonly IPaymobService _paymobService;
    private readonly PaymobSettings _paymobSettings;

    public PaymobCallbackQueryHandler(
        IPaymobService paymobService,
        PaymobSettings paymobSettings) : base()
    {
        _paymobService = paymobService;
        _paymobSettings = paymobSettings;
    }

    public Task<PaymobCallbackResponse> Handle(PaymobCallbackQuery request, CancellationToken cancellationToken)
    {
        string[] fields = new[]
        {
            request.AmountCents, request.CreatedAt, request.Currency, request.ErrorOccured,
            request.HasParentTransaction, request.Id, request.IntegrationId, request.Is3dSecure,
            request.IsAuth, request.IsCapture, request.IsRefunded, request.IsStandalonePayment,
            request.IsVoided, request.Order, request.Owner, request.Pending,
            request.SourceDataPan, request.SourceDataSubType, request.SourceDataType, request.Success
        };

        var concatenated = string.Concat(fields);
        string calculatedHmac = _paymobService.ComputeHmacSHA512(concatenated, _paymobSettings.HMAC);

        if (!request.Hmac.Equals(calculatedHmac, StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(new PaymobCallbackResponse(
                false,
                HtmlGenerator.GenerateSecurityHtml()
            ));
        }

        bool.TryParse(request.Success, out bool isSuccess);

        return Task.FromResult(new PaymobCallbackResponse(
            isSuccess,
            isSuccess ? HtmlGenerator.GenerateSuccessHtml() : HtmlGenerator.GenerateFailedHtml()
        ));
    }
}

