using Microsoft.AspNetCore.Mvc;

namespace Dotnet_3.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
