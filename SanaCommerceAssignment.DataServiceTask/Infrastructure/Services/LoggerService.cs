using SanaCommerceAssignment.DataServiceTask.Infrastructure.Services.Interfaces;
namespace SanaCommerceAssignment.DataServiceTask.Infrastructure.Services;
public class LoggerService(
    ILogger<LoggerService> logger) : ILoggerService
{
    public void LogInformation(string message)
    {
        logger.LogInformation(message);
    }
    public void LogError(string message, Exception exception)
    {
        logger.LogError(exception, message);
    }
}