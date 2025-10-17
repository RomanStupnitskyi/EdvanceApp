using ContentService.SharedKernel;
using ContentService.Application.Messaging;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace ContentService.Application.Abstractions.Behaviors;

public static class LoggingDecorator
{
    private static readonly Action<ILogger, string, Exception?> ProcessingCommand =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(1, nameof(ProcessingCommand)),
            "Processing command {Command}");

    private static readonly Action<ILogger, string, Exception?> CompletedCommand =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(2, nameof(CompletedCommand)),
            "Completed command {Command}");

    private static readonly Action<ILogger, string, Exception?> CompletedCommandWithError =
        LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(3, nameof(CompletedCommandWithError)),
            "Completed command {Command} with error");

    private static readonly Action<ILogger, string, Exception?> ProcessingQuery =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(4, nameof(ProcessingQuery)),
            "Processing query {Query}");

    private static readonly Action<ILogger, string, Exception?> CompletedQuery =
        LoggerMessage.Define<string>(
            LogLevel.Information,
            new EventId(5, nameof(CompletedQuery)),
            "Completed query {Query}");

    private static readonly Action<ILogger, string, Exception?> CompletedQueryWithError =
        LoggerMessage.Define<string>(
            LogLevel.Error,
            new EventId(6, nameof(CompletedQueryWithError)),
            "Completed query {Query} with error");

    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> innerHandler,
        ILogger<CommandHandler<TCommand, TResponse>> logger)
        : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var commandName = typeof(TCommand).Name;
            
            ProcessingCommand(logger, commandName, null);
            
            var result = await innerHandler.Handle(command, cancellationToken).ConfigureAwait(false);
            
            if (result.IsSuccess)
                CompletedCommand(logger, commandName, null);
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    CompletedCommandWithError(logger, commandName, null);
                }
            }

            return result;
        }
    }

    internal sealed class CommandBaseHandler<TCommand>(
        ICommandHandler<TCommand> innerHandler,
        ILogger<CommandBaseHandler<TCommand>> logger)
        : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var commandName = typeof(TCommand).Name;
            
            ProcessingCommand(logger, commandName, null);
            
            var result = await innerHandler.Handle(command, cancellationToken).ConfigureAwait(false);
            
            if (result.IsSuccess)
                CompletedCommand(logger, commandName, null);
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    CompletedCommandWithError(logger, commandName, null);
                }
            }

            return result;
        }
    }
    
    internal sealed class QueryHandler<TQuery, TResponse>(
        IQueryHandler<TQuery, TResponse> innerHandler,
        ILogger<QueryHandler<TQuery, TResponse>> logger)
        : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
        {
            var queryName = typeof(TQuery).Name;
            
            ProcessingQuery(logger, queryName, null);
            
            var result = await innerHandler.Handle(query, cancellationToken).ConfigureAwait(false);
            
            if (result.IsSuccess)
                CompletedQuery(logger, queryName, null);
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    CompletedQueryWithError(logger, queryName, null);
                }
            }

            return result;
        }
    }
}