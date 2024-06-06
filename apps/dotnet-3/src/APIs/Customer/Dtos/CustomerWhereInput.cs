namespace Dotnet_3.APIs.Dtos;

public class CustomerWhereInput
{
    public string? Id { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public List<ProductIdDto>? Products { get; set; }
}
