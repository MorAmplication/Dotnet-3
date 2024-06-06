using Dotnet_3.APIs;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_3.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class MorControllerBase : ControllerBase
{
    protected readonly IMorService _service;

    public MorControllerBase(IMorService service)
    {
        _service = service;
    }

    [HttpPost()]
    public async Task<string> CreateMor([FromBody()] string data)
    {
        return await _service.CreateMor(data);
    }

    [HttpGet()]
    public async Task<string> GetMor([FromRoute()] string data)
    {
        return await _service.GetMor(data);
    }
}
