using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Users.Delete;

internal sealed class DeleteCommandHandler : IRequestHandler<DeleteCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly ISoftDeleteUserService _softDeleteUserService;

    public DeleteCommandHandler(IUserRepository userRepository, ISoftDeleteUserService softDeleteUserService)
    {
        _userRepository = userRepository;
        _softDeleteUserService = softDeleteUserService;
    }

    public async Task<Result> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var id = request.Id;

        var isExisted = await _userRepository.IsExisted(id);
        if(!isExisted)
        {
            return Result.Failure(UserError.UserNotFound);
        }

        await _softDeleteUserService.DeleteAsync(id);

        return Result.Success();
    }
}
