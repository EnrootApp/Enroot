using Enroot.Domain.Common.Models;
using Enroot.Domain.User.ValueObjects;

namespace Enroot.Domain.Unit.Common.Tests;

public class ValueObjectTests
{
    [Fact]
    public void EqualityTest()
    {
        ValueObject email = Email.Create("abc@mail.ru");
        ValueObject secondEmail = Email.Create("abc@mail.ru");

        Assert.Equal(email, secondEmail);
    }
}