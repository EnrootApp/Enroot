namespace Enroot.Contracts.Tenant;

public record TenantResponse(string Id, string Name, string LogoUrl, string[] AccountIds);