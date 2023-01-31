namespace Enroot.Contracts.Tenant;

public class CreateTenantResponse
{
    public string Id { get; init; } = default!;
    public string Name { get; init; } = default!;
}