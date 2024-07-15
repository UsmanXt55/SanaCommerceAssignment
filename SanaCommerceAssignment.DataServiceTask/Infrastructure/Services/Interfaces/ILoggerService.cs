namespace SanaCommerceAssignment.DataServiceTask.Infrastructure.Services.Interfaces;
public interface ILoggerService
{
    void LogInformation(string message);
    void LogError(string message, Exception exception);
}
