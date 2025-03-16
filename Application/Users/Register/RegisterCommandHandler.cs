using Domain.Shared;
using Domain.Users;
using MediatR;

namespace Application.Users.Register;

internal sealed class RegisterCommandHandler : IRequestHandler<RegisterCommand, Result<RegisterResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly Abstractions.IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUserRepository userRepository, Abstractions.IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<RegisterResponse>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var newUser = User.CreateForRegistration(request.Name, request.Password);

        _userRepository.Add(newUser);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var response = new RegisterResponse("test access token", "test refresh token");

        return Result.Success(response);
    }
}
