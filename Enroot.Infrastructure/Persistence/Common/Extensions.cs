using System.ComponentModel;
using System.Reflection;
using Enroot.Domain.Common.Interfaces;
using Enroot.Domain.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enroot.Infrastructure.Persistence.Common;

public static class Extensions
{
    public static void SeedEnumValues<T, TEnum>(this EntityTypeBuilder<T> builder, Func<TEnum, T> converter)
        where T : class
    {
        var values = Enum.GetValues(typeof(TEnum))
            .Cast<object>()
            .Select(value => converter((TEnum)value))
            .ToList();

        builder.HasData(values);
    }

    public static string GetEnumDescriptionOrName(this Enum e)
    {
        var name = e.ToString();
        var memberInfo = e.GetType().GetMember(name)[0];
        var descriptionAttributes = memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);

        if (!descriptionAttributes.Any())
            return name;

        return ((DescriptionAttribute)descriptionAttributes[0]).Description;
    }

    public static async Task DispatchDomainEventsAsync<TId>(this IMediator mediator, EnrootContext ctx)
    where TId : ValueObject
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<AggregateRoot<TId>>()
            .Where(x => x.Entity.DomainEvents?.Any() == true);

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        var tasks = domainEvents
            .Select((domainEvent) => Task.Run(() => mediator.Publish(domainEvent)));

        await Task.WhenAll(tasks);
    }
}