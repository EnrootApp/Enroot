using Enroot.Application.Services;
using Bytewizer.Backblaze.Client;
using Microsoft.Extensions.Configuration;
using Bytewizer.Backblaze.Models;
using ErrorOr;

namespace Enroot.Infrastructure.Services;

public sealed class CloudStorage : ICloudStorage
{
    private readonly IStorageClient _storage;
    private readonly IConfiguration _configuration;

    public CloudStorage(IStorageClient storage, IConfiguration configuration)
    {
        _storage = storage;
        _configuration = configuration;
    }

    public async Task<ErrorOr<string>> UploadAsync(string name, byte[] file, CancellationToken cancellationToken)
    {
        var request = new UploadFileByBucketIdRequest(_configuration["CloudStorage:BucketId"], name);
        using var stream = new MemoryStream(file);

        var result = await _storage.UploadAsync(request, stream, null, cancellationToken);

        if (!result.IsSuccessStatusCode)
        {
            return Domain.Common.Errors.Errors.Attachment.UploadFail;
        }

        return _configuration["CloudStorage:RootUrl"] + result.Response.FileId;
    }
}