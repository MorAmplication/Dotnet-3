using Dotnet_3.APIs;
using Dotnet_3.APIs.Dtos;
using Dotnet_3.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_3.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ProductsControllerBase : ControllerBase
{
    protected readonly IProductsService _service;

    public ProductsControllerBase(IProductsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Product
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<ProductDto>> CreateProduct(ProductCreateInput input)
    {
        var product = await _service.CreateProduct(input);

        return CreatedAtAction(nameof(Product), new { id = product.Id }, product);
    }

    /// <summary>
    /// Delete one Product
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteProduct([FromRoute()] ProductIdDto idDto)
    {
        try
        {
            await _service.DeleteProduct(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Products
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<ProductDto>>> Products([FromQuery()] ProductFindMany filter)
    {
        return Ok(await _service.Products(filter));
    }

    /// <summary>
    /// Get one Product
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<ProductDto>> Product([FromRoute()] ProductIdDto idDto)
    {
        try
        {
            return await _service.Product(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get a Customer record for Product
    /// </summary>
    [HttpGet("{Id}/customers")]
    public async Task<ActionResult<List<CustomerDto>>> GetCustomer([FromRoute()] ProductIdDto idDto)
    {
        var customer = await _service.GetCustomer(idDto);
        return Ok(customer);
    }

    /// <summary>
    /// Update one Product
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateProduct(
        [FromRoute()] ProductIdDto idDto,
        [FromQuery()] ProductUpdateInput productUpdateDto
    )
    {
        try
        {
            await _service.UpdateProduct(idDto, productUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
