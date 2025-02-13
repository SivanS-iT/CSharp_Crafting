using Domain.Shared;
using MediatR;

namespace Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
    :  IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}