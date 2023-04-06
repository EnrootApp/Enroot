using System.ComponentModel.DataAnnotations;

namespace Enroot.Domain.Common.Models;

public abstract class ReadEntity
{
    public Guid Id { get; private set; }
    public int DbId { get; private set; }
}