namespace Enroot.Contracts.Tenant;

public record GetTenantsRequest(
    int Offset,
    int Take,
    string? Name);