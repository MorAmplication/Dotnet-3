using Dotnet_3.APIs;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_3.APIs;

[ApiController()]
public class MorsController : MorsControllerBase
{
    public MorsController(IMorsService service)
        : base(service) { }
}
