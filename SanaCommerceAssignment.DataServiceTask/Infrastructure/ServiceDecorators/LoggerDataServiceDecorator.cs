using SanaCommerceAssignment.DataServiceTask.Infrastructure.Services.Interfaces;
namespace SanaCommerceAssignment.DataServiceTask.Infrastructure.ServiceDecorators;
public class LoggerDataServiceDecorator(
    IDataService dataService, 
    ILoggerService loggerService) : IDataService
{
    public IEnumerable<string> GetLines()
    {
        var data = dataService.GetLines();
        loggerService.LogInformation($"{string.Join(Environment.NewLine, data)}");
        return data;
    }
}
