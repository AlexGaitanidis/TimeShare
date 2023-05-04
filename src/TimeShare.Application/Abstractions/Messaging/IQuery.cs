using ErrorOr;
using MediatR;

namespace TimeShare.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{
}