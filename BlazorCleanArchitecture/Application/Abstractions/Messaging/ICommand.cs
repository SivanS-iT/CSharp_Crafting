using Domain.Shared;
using MediatR;

namespace Application.Abstractions.Messaging;

/// <summary>
/// Represents a command in the CQRS pattern
/// </summary>
public interface ICommand : IRequest<Result>, IBaseCommand;


/// <summary>
/// Represents a command in the CQRS pattern that returns a result with a specific type
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;


/// <summary>
/// A marker interface for all command types in the CQRS patter
/// </summary>
public interface IBaseCommand;