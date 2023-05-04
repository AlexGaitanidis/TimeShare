using ErrorOr;
using MediatR;

namespace TimeShare.Application.Abstractions.Messaging;

public interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>
{
}