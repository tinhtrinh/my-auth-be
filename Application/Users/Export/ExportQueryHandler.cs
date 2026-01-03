using Application.Shared.Auth;
using Application.Shared.Background;
using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Users.Export;

internal class ExportQueryHandler : IRequestHandler<ExportQuery, Result>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IBackgroundService _backgroundService;

    public ExportQueryHandler(IJwtProvider jwtProvider, IBackgroundService backgroundService)
    {
        _jwtProvider = jwtProvider;
        _backgroundService = backgroundService;
    }

    public Task<Result> Handle(ExportQuery request, CancellationToken cancellationToken)
    {
        var connectionId = request.ConnectionId;
        var token = request.Token;

        if (token is null)
        {
            return Task.FromResult(Result.Failure(UserError.UserNotFound));
        }

        var userId = _jwtProvider.GetUserIdFromToken(token);

        if(string.IsNullOrEmpty(userId))
        {
            return Task.FromResult(Result.Failure(UserError.UserNotFound));
        }

        _backgroundService.Enqueue<ExportAndNotifyHanlder>(hander => hander.ExcuteAsync(connectionId, userId, cancellationToken));

        return Task.FromResult(Result.Success());
    }
}
