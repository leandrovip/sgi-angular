using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vip.SGI.Domain.Models;
using Vip.SGI.Domain.ValueObjects;

namespace Vip.SGI.Infra.Mappings;

public class UsuarioMap : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario");

        #region Propriedades

        builder.HasKey(x => x.UsuarioId);
        builder.Property(x => x.Nome).HasMaxLength(Usuario.NomeMaxLength).IsRequired();
        builder.Property(x => x.Senha).HasMaxLength(Usuario.SenhaMaxLength).IsRequired();
        builder.OwnsOne(x => x.Email).Property(x => x.Endereco).HasColumnName("Email").HasMaxLength(Email.EnderecoMaxLength).IsRequired();

        #endregion

        #region Índices

        builder.HasIndex(x => x.Nome).HasDatabaseName("IX_Usuario_Nome").IsUnique(false);
        builder.OwnsOne(x => x.Email).HasIndex(x => x.Endereco).HasDatabaseName("IX_Usuario_Email").IsUnique(false);

        #endregion
    }
}