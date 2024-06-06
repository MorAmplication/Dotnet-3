using Dotnet_3.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_3.Infrastructure;

public class Dotnet_3DbContext : DbContext
{
    public Dotnet_3DbContext(DbContextOptions<Dotnet_3DbContext> options)
        : base(options) { }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Product> Products { get; set; }
}
