using Domain.Shared;
using MediatR;

namespace Application.Users.SendVerifyEmail;

public record SendVerifyEmailCommand : IRequest<Result>;