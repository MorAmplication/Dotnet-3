using Dotnet_3.APIs.Dtos;

namespace Dotnet_3.APIs;

public interface IProductsService
{
    /// <summary>
    /// Create one Product
    /// </summary>
    public Task<ProductDto> CreateProduct(ProductCreateInput productDto);

    /// <summary>
    /// Delete one Product
    /// </summary>
    public Task DeleteProduct(ProductIdDto idDto);

    /// <summary>
    /// Find many Products
    /// </summary>
    public Task<List<ProductDto>> Products(ProductFindMany findManyArgs);

    /// <summary>
    /// Get one Product
    /// </summary>
    public Task<ProductDto> Product(ProductIdDto idDto);

    /// <summary>
    /// Get a Customer record for Product
    /// </summary>
    public Task<CustomerDto> GetCustomer(ProductIdDto idDto);

    /// <summary>
    /// Update one Product
    /// </summary>
    public Task UpdateProduct(ProductIdDto idDto, ProductUpdateInput updateDto);
}
