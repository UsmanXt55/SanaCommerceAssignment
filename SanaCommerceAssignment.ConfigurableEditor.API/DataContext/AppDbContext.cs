using Microsoft.EntityFrameworkCore;
using SanaCommerceAssignment.ConfigurableEditor.API.Models.Entities;
namespace SanaCommerceAssignment.ConfigurableEditor.API.DataContext;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
   : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TemplateEntity>().HasKey(x => x.Id);
    }

    public DbSet<TemplateEntity> Templates { get; set; }
}
