using Microsoft.EntityFrameworkCore;
using SanaCommerceAssignment.ConfigurableEditor.API.Models.DOs;
using SanaCommerceAssignment.ConfigurableEditor.API.Models.Entities;
namespace SanaCommerceAssignment.ConfigurableEditor.API.DataContext.Repositories;
public interface ITemplateRepository
{
    Task<IEnumerable<TemplateEntity>> QueryAsync(TemplateQueryDo query, CancellationToken cancellationToken);
    Task CreateAsync(IEnumerable<TemplateEntity> templateList, CancellationToken cancellationToken);
    Task CreateAsync(TemplateEntity template, CancellationToken cancellationToken);
    Task<TemplateEntity> UpdateAsync(TemplateEntity template, CancellationToken cancellationToken);
}

public class TemplateRepository(
    AppDbContext dbContext) : ITemplateRepository
{
    public async Task<IEnumerable<TemplateEntity>> QueryAsync(TemplateQueryDo query, CancellationToken cancellationToken)
    {
        try
        {
            var templateQuery = dbContext.Templates.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.PageId))
                templateQuery = templateQuery.Where(x => x.PageId == query.PageId);
            if (!string.IsNullOrWhiteSpace(query.FieldId))
                templateQuery = templateQuery.Where(x => x.FieldId == query.FieldId);

            return await templateQuery.ToListAsync(cancellationToken);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task CreateAsync(IEnumerable<TemplateEntity> templateList, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Templates.AddRangeAsync(templateList, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task CreateAsync(TemplateEntity template, CancellationToken cancellationToken)
    {
        try
        {
            await dbContext.Templates.AddAsync(template, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<TemplateEntity> UpdateAsync(TemplateEntity template, CancellationToken cancellationToken)
    {
        try
        {
            dbContext.Templates.Update(template);
            await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return template;
        }
        catch (Exception)
        {
            throw;
        }
    }
}