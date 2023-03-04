using ErrorOr;

namespace Enroot.Application.Services;

public interface ICloudStorage
{
    Task<ErrorOr<string>> UploadAsync(string name, byte[] file, CancellationToken cancellationToken);
}