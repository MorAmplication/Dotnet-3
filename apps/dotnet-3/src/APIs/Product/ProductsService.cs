using Dotnet_3.Infrastructure;

namespace Dotnet_3.APIs;

public class ProductsService : ProductsServiceBase
{
    public ProductsService(Dotnet_3DbContext context)
        : base(context) { }
}
