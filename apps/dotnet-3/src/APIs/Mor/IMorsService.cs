namespace Dotnet_3.APIs;

public interface IMorsService
{
    public Task<string> CreateMor(string data);
    public Task<string> GetMor(string data);
}
