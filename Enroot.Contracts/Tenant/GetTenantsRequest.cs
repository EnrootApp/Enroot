namespace Enroot.Contracts.Tenant;

public record GetTenantsRequest(
    Guid UserId,
    int Offset,
    int Limit,
    string Name);