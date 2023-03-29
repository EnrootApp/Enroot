namespace Enroot.Contracts.Tenant;

public record GetTenantsRequest(
    int Skip,
    int Take,
    string? Name);