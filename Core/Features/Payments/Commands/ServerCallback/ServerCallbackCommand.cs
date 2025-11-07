using System.Text.Json;

namespace Core.Features.Payments.Commands.ServerCallback;

public record ServerCallbackCommand(JsonElement Payload, string Hmac) : IRequest<ApiResponse<string>>;

