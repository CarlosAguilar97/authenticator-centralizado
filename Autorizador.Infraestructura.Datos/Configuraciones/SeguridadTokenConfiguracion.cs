using Autorizador.Dominio.Entidades.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Infraestructura.Datos.Configuraciones
{
    public class SeguridadTokenConfiguracion:   IEntityTypeConfiguration<SeguridadToken>
    {
        public void Configure(EntityTypeBuilder<SeguridadToken> builder)
    {
        builder.ToTable("SEG_TOKEN_REFRESCO");
        builder.HasKey(u => u.IdentificadorToken);

        builder.Property(p => p.IdentificadorToken).HasColumnName("ID_TOKEN").IsRequired().ValueGeneratedOnAdd();
        builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO").IsRequired();
        builder.Property(p => p.ValorToken).HasColumnName("VAL_TOKEN").IsRequired();
        builder.Property(p => p.FechaExpiracion).HasColumnName("FEC_EXPIRACION").IsRequired();
        builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO").IsRequired().HasMaxLength(1).HasDefaultValue("1");
        builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO").IsRequired().HasDefaultValueSql("GETDATE()");
    }
}
}
