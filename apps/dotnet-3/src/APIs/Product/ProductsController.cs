using Microsoft.AspNetCore.Mvc;

namespace Dotnet_3.APIs;

[ApiController()]
public class ProductsController : ProductsControllerBase
{
    public ProductsController(IProductsService service)
        : base(service) { }
}
