using Enroot.Domain.Common.Models;
using Enroot.Domain.Common.Errors;
using ErrorOr;

namespace Enroot.Domain.Tasq.ValueObjects;

public sealed class Attachment : ValueObject
{
    private const int _maxNameLength = 64;

    public string BlobUrl { get; }
    public string Name { get; }

    private Attachment(string name, string blobUrl)
    {
        Name = name;
        BlobUrl = blobUrl;
    }

    public static ErrorOr<Attachment> Create(string name, string blobUrl)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length > _maxNameLength)
        {
            return Errors.Tasq.AttachmentNameInvalid;
        }

        if (string.IsNullOrWhiteSpace(blobUrl))
        {
            return Errors.Tasq.AttachmentUrlInvalid;
        }

        return new Attachment(name, blobUrl);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return BlobUrl;
        yield return Name;
    }
}