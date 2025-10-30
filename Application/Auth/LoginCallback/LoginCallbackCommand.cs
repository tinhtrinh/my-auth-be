using Domain.Shared;
using MediatR;

namespace Application.Auth.LoginCallback;

public record LoginCallbackCommand(string Code) : IRequest<Result<LoginCallbackResponse>>;
