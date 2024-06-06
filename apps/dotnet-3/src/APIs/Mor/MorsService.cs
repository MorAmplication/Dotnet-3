using Dotnet_3.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_3.APIs;

public class MorsService : MorsServiceBase
{
    public MorsService(Dotnet_3DbContext context)
        : base(context) { }
}
