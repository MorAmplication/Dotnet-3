using Dotnet_3.APIs.Dtos;

namespace Dotnet_3.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one Customer
    /// </summary>
    public Task<CustomerDto> CreateCustomer(CustomerCreateInput customerDto);
    public Task<string> CustomCreate(string data);

    /// <summary>
    /// Connect multiple Products records to Customer
    /// </summary>
    public Task ConnectProducts(CustomerIdDto idDto, ProductIdDto[] productsId);

    /// <summary>
    /// Disconnect multiple Products records from Customer
    /// </summary>
    public Task DisconnectProducts(CustomerIdDto idDto, ProductIdDto[] productsId);

    /// <summary>
    /// Find multiple Products records for Customer
    /// </summary>
    public Task<List<ProductDto>> FindProducts(
        CustomerIdDto idDto,
        ProductFindMany ProductFindMany
    );

    /// <summary>
    /// Update multiple Products records for Customer
    /// </summary>
    public Task UpdateProducts(CustomerIdDto idDto, ProductIdDto[] productsId);
    public Task<string> CustomGet(string data);

    /// <summary>
    /// Delete one Customer
    /// </summary>
    public Task DeleteCustomer(CustomerIdDto idDto);

    /// <summary>
    /// Find many Customers
    /// </summary>
    public Task<List<CustomerDto>> Customers(CustomerFindMany findManyArgs);

    /// <summary>
    /// Get one Customer
    /// </summary>
    public Task<CustomerDto> Customer(CustomerIdDto idDto);

    /// <summary>
    /// Update one Customer
    /// </summary>
    public Task UpdateCustomer(CustomerIdDto idDto, CustomerUpdateInput updateDto);
}
