using Dotnet_3.APIs.Common;
using Dotnet_3.Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_3.APIs.Dtos;

[BindProperties(SupportsGet = true)]
public class ProductFindMany : FindManyInput<Product, ProductWhereInput> { }
