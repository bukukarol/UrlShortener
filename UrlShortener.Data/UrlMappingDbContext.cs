using Microsoft.EntityFrameworkCore;
using UrlShortener.Domain;

namespace UrlShortener.Data;

public class UrlMappingDbContext : DbContext
{
    public UrlMappingDbContext(DbContextOptions<UrlMappingDbContext> options):base(options) { }
    public DbSet<UrlMapping> UrlMappings { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UrlMapping>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<UrlMapping>()
            .OwnsOne(x => x.Code, xx =>
            {
                xx.HasIndex(x => x.Value).IsUnique();
                xx.Property(x => x.Value).IsRequired();
            });

        modelBuilder.Entity<UrlMapping>()
            .OwnsOne(x => x.Url)
            .Property(x=>x.Value)
            .IsRequired();

       
    }
}