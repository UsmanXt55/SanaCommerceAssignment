using SanaCommerceAssignment.DataServiceTask.Infrastructure.Services.Interfaces;
namespace SanaCommerceAssignment.DataServiceTask.Infrastructure.Services;
public class FileDataService(string filePath) : IDataService
{
    public IEnumerable<string> GetLines()
    {
        try
		{
            return File.ReadAllLines(filePath);
        }
		catch (Exception)
		{
            throw;
		}
    }
}