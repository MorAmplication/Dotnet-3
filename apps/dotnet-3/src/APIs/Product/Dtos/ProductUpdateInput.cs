namespace Dotnet_3.APIs.Dtos;

public class ProductUpdateInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public CustomerIdDto? Customer { get; set; }
}
