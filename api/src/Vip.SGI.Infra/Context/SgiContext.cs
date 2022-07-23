using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Vip.SGI.Domain.Enums;
using Vip.SGI.Domain.Models;
using Vip.SGI.Domain.ValueObjects;
using Vip.SGI.Infra.Extensions;
using Vip.Validator.Notifications;

namespace Vip.SGI.Infra.Context;

public class SgiContext : DbContext
{
    #region Propriedades

    public DbSet<Usuario> Usuario { get; set; }

    #endregion

    #region Construtores

    public SgiContext(DbContextOptions<SgiContext> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
        ChangeTracker.CascadeDeleteTiming = CascadeTiming.Never;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    #endregion

    #region Métodos

    public void Seed()
    {
        var retorno = Usuario.AsNoTracking().Any();
        if (!retorno)
        {
            var usuario = new Usuario(Guid.NewGuid(), "Admin", "admin@admin.com", "123", UsuarioFuncao.Administrador);
            Usuario.Add(usuario);
            SaveChanges();
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Ignore class ValueObjects and Validators

        modelBuilder.Ignore<Notification>();
        modelBuilder.Ignore<Notifiable>();
        modelBuilder.Ignore<Email>();
        modelBuilder.Ignore<CpfCnpj>();
        modelBuilder.Ignore<Telefone>();
        modelBuilder.Ignore<Endereco>();
        modelBuilder.Ignore<Cep>();

        #endregion

        #region Apply Mappings

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        #endregion

        #region Convetions

        modelBuilder.AddRemoveOneToManyCascadeConvention();
        modelBuilder.AddDefaultVarcharConvention();
        modelBuilder.AddDefaultDecimalConvention();
        modelBuilder.AddValueObjectConvetion();
        modelBuilder.ApplyConventions();

        #endregion

        base.OnModelCreating(modelBuilder);
    }

    #endregion
}