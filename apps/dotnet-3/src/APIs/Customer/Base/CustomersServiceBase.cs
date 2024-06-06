using Dotnet_3.APIs;
using Dotnet_3.APIs.Common;
using Dotnet_3.APIs.Dtos;
using Dotnet_3.APIs.Errors;
using Dotnet_3.APIs.Extensions;
using Dotnet_3.Infrastructure;
using Dotnet_3.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_3.APIs;

public abstract class CustomersServiceBase : ICustomersService
{
    protected readonly Dotnet_3DbContext _context;

    public CustomersServiceBase(Dotnet_3DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    public async Task<CustomerDto> CreateCustomer(CustomerCreateInput createDto)
    {
        var customer = new Customer
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            customer.Id = createDto.Id;
        }
        if (createDto.Products != null)
        {
            customer.Products = await _context
                .Products.Where(product =>
                    createDto.Products.Select(t => t.Id).Contains(product.Id)
                )
                .ToListAsync();
        }

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<Customer>(customer.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    public async Task<string> CustomCreate(string data)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Connect multiple Products records to Customer
    /// </summary>
    public async Task ConnectProducts(CustomerIdDto idDto, ProductIdDto[] productsId)
    {
        var customer = await _context
            .Customers.Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var products = await _context
            .Products.Where(t => productsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (products.Count == 0)
        {
            throw new NotFoundException();
        }

        var productsToConnect = products.Except(customer.Products);

        foreach (var product in productsToConnect)
        {
            customer.Products.Add(product);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Products records from Customer
    /// </summary>
    public async Task DisconnectProducts(CustomerIdDto idDto, ProductIdDto[] productsId)
    {
        var customer = await _context
            .Customers.Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var products = await _context
            .Products.Where(t => productsId.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var product in products)
        {
            customer.Products?.Remove(product);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Products records for Customer
    /// </summary>
    public async Task<List<ProductDto>> FindProducts(
        CustomerIdDto idDto,
        ProductFindMany customerFindMany
    )
    {
        var products = await _context
            .Products.Where(m => m.CustomerId == idDto.Id)
            .ApplyWhere(customerFindMany.Where)
            .ApplySkip(customerFindMany.Skip)
            .ApplyTake(customerFindMany.Take)
            .ApplyOrderBy(customerFindMany.SortBy)
            .ToListAsync();

        return products.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Products records for Customer
    /// </summary>
    public async Task UpdateProducts(CustomerIdDto idDto, ProductIdDto[] productsId)
    {
        var customer = await _context
            .Customers.Include(t => t.Products)
            .FirstOrDefaultAsync(x => x.Id == idDto.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        var products = await _context
            .Products.Where(a => productsId.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (products.Count == 0)
        {
            throw new NotFoundException();
        }

        customer.Products = products;
        await _context.SaveChangesAsync();
    }

    public async Task<string> CustomGet(string data)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public async Task DeleteCustomer(CustomerIdDto idDto)
    {
        var customer = await _context.Customers.FindAsync(idDto.Id);
        if (customer == null)
        {
            throw new NotFoundException();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    public async Task<List<CustomerDto>> Customers(CustomerFindMany findManyArgs)
    {
        var customers = await _context
            .Customers.Include(x => x.Products)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return customers.ConvertAll(customer => customer.ToDto());
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    public async Task<CustomerDto> Customer(CustomerIdDto idDto)
    {
        var customers = await this.Customers(
            new CustomerFindMany { Where = new CustomerWhereInput { Id = idDto.Id } }
        );
        var customer = customers.FirstOrDefault();
        if (customer == null)
        {
            throw new NotFoundException();
        }

        return customer;
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    public async Task UpdateCustomer(CustomerIdDto idDto, CustomerUpdateInput updateDto)
    {
        var customer = updateDto.ToModel(idDto);

        if (updateDto.Products != null)
        {
            customer.Products = await _context
                .Products.Where(product =>
                    updateDto.Products.Select(t => t.Id).Contains(product.Id)
                )
                .ToListAsync();
        }

        _context.Entry(customer).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Customers.Any(e => e.Id == customer.Id))
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
