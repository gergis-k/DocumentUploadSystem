using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Database.Contexts;

public sealed class SystemDatabase : DbContext
{
    public DbSet<Document> Documents { get; set; }
    public DbSet<UploadedFile> UploadedFiles { get; set; }
    public DbSet<AllowedExtension> AllowedExtensions { get; set; }

    public SystemDatabase(DbContextOptions<SystemDatabase> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
            x => x.Namespace != "Database.Configurations.Bases");
    }
}
