using Dotnet_3.APIs;
using Dotnet_3.APIs.Common;
using Dotnet_3.APIs.Dtos;
using Dotnet_3.APIs.Errors;
using Dotnet_3.APIs.Extensions;
using Dotnet_3.Infrastructure;
using Dotnet_3.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_3.APIs;

public abstract class ProductsServiceBase : IProductsService
{
    protected readonly Dotnet_3DbContext _context;

    public ProductsServiceBase(Dotnet_3DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Product
    /// </summary>
    public async Task<ProductDto> CreateProduct(ProductCreateInput createDto)
    {
        var product = new Product
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            product.Id = createDto.Id;
        }
        if (createDto.Customer != null)
        {
            product.Customer = await _context
                .Customers.Where(customer => createDto.Customer.Id == customer.Id)
                .FirstOrDefaultAsync();
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Product>(product.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Product
    /// </summary>
    public async Task DeleteProduct(ProductIdDto idDto)
    {
        var product = await _context.Products.FindAsync(idDto.Id);
        if (product == null)
        {
            throw new NotFoundException();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Products
    /// </summary>
    public async Task<List<ProductDto>> Products(ProductFindMany findManyArgs)
    {
        var products = await _context
            .Products.Include(x => x.Customer)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return products.ConvertAll(product => product.ToDto());
    }

    /// <summary>
    /// Get one Product
    /// </summary>
    public async Task<ProductDto> Product(ProductIdDto idDto)
    {
        var products = await this.Products(
            new ProductFindMany { Where = new ProductWhereInput { Id = idDto.Id } }
        );
        var product = products.FirstOrDefault();
        if (product == null)
        {
            throw new NotFoundException();
        }

        return product;
    }

    /// <summary>
    /// Get a Customer record for Product
    /// </summary>
    public async Task<CustomerDto> GetCustomer(ProductIdDto idDto)
    {
        var product = await _context
            .Products.Where(product => product.Id == idDto.Id)
            .Include(product => product.Customer)
            .FirstOrDefaultAsync();
        if (product == null)
        {
            throw new NotFoundException();
        }
        return product.Customer.ToDto();
    }

    /// <summary>
    /// Update one Product
    /// </summary>
    public async Task UpdateProduct(ProductIdDto idDto, ProductUpdateInput updateDto)
    {
        var product = updateDto.ToModel(idDto);

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Products.Any(e => e.Id == product.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
