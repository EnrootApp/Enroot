using Enroot.Domain.Common.Models;
using Enroot.Domain.User.ValueObjects;

namespace Enroot.Domain.Unit.Common.Tests;

public class ValueObjectTests
{
    [Fact]
    public void EqualityTest()
    {
        ValueObject email = Email.Create("abc@mail.ru").Value;
        ValueObject secondEmail = Email.Create("abc@mail.ru").Value;

        Assert.Equal(email, secondEmail);
    }
}