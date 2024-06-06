using Dotnet_3.Infrastructure;

namespace Dotnet_3.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(Dotnet_3DbContext context)
        : base(context) { }
}
