using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Vip.SGI.Infra.Extensions;

public static class ContextExtensions
{
    private static readonly List<Action<IMutableEntityType>> Conventions = new();

    public static void AddDefaultDecimalConvention(this ModelBuilder builder)
    {
        var decimalProperties = builder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(decimal)));
        foreach (var property in decimalProperties) property.SetColumnType("decimal(12,2)");
    }

    public static void AddDefaultVarcharConvention(this ModelBuilder builder)
    {
        var stringProperties = builder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)));
        foreach (var property in stringProperties) property.SetColumnType("varchar(100)");
    }

    public static void AddValueObjectConvetion(this ModelBuilder builder)
    {
        foreach (var relationship in builder.Model.GetEntityTypes().Where(e => e.IsOwned()).SelectMany(e => e.GetForeignKeys())) 
            relationship.DeleteBehavior = DeleteBehavior.ClientNoAction;
    }

    public static void AddRemoveOneToManyCascadeConvention(this ModelBuilder builder)
    {
        Conventions.Add(et => et.GetForeignKeys()
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
            .ToList()
            .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.ClientNoAction));
    }

    public static void ApplyConventions(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        foreach (var action in Conventions)
            action(entityType);

        Conventions.Clear();
    }
}