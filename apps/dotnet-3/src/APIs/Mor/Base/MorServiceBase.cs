using Dotnet_3.APIs;
using Dotnet_3.Infrastructure;
using Dotnet_3.Infrastructure.Models;

namespace Dotnet_3.APIs;

public abstract class MorServiceBase : IMorService
{
    protected readonly Dotnet_3DbContext _context;

    public MorServiceBase(Dotnet_3DbContext context)
    {
        _context = context;
    }

    public async Task<string> CreateMor(string data)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetMor(string data)
    {
        throw new NotImplementedException();
    }
}
