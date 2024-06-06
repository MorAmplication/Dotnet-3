using Dotnet_3.APIs;
using Dotnet_3.APIs.Dtos;
using Dotnet_3.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_3.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    [HttpGet()]
    public async Task<string> CustomCreate([FromQuery()] string data)
    {
        return await _service.CustomCreate(data);
    }

    [HttpGet()]
    public async Task<string> CustomGet([FromRoute()] string data)
    {
        return await _service.CustomGet(data);
    }

    /// <summary>
    /// Connect multiple Products records to Customer
    /// </summary>
    [HttpPost("{Id}/products")]
    public async Task<ActionResult> ConnectProducts(
        [FromRoute()] CustomerIdDto idDto,
        [FromQuery()] ProductIdDto[] productsId
    )
    {
        try
        {
            await _service.ConnectProducts(idDto, productsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Products records from Customer
    /// </summary>
    [HttpDelete("{Id}/products")]
    public async Task<ActionResult> DisconnectProducts(
        [FromRoute()] CustomerIdDto idDto,
        [FromBody()] ProductIdDto[] productsId
    )
    {
        try
        {
            await _service.DisconnectProducts(idDto, productsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Products records for Customer
    /// </summary>
    [HttpGet("{Id}/products")]
    public async Task<ActionResult<List<ProductDto>>> FindProducts(
        [FromRoute()] CustomerIdDto idDto,
        [FromQuery()] ProductFindMany filter
    )
    {
        try
        {
            return Ok(await _service.FindProducts(idDto, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Products records for Customer
    /// </summary>
    [HttpPatch("{Id}/products")]
    public async Task<ActionResult> UpdateProducts(
        [FromRoute()] CustomerIdDto idDto,
        [FromBody()] ProductIdDto[] productsId
    )
    {
        try
        {
            await _service.UpdateProducts(idDto, productsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet()]
    public async Task<string> CustomCreate([FromQuery()] string data)
    {
        return await _service.CustomCreate(data);
    }

    [HttpGet()]
    public async Task<string> CustomGet([FromRoute()] string data)
    {
        return await _service.CustomGet(data);
    }

    /// <summary>
    /// Delete one Customer
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerIdDto idDto)
    {
        try
        {
            await _service.DeleteCustomer(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<CustomerDto>>> Customers(
        [FromQuery()] CustomerFindMany filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<CustomerDto>> Customer([FromRoute()] CustomerIdDto idDto)
    {
        try
        {
            return await _service.Customer(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerIdDto idDto,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(idDto, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
