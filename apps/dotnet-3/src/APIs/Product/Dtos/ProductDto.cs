namespace Dotnet_3.APIs.Dtos;

public class ProductDto : ProductIdDto
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public CustomerIdDto? Customer { get; set; }
}
