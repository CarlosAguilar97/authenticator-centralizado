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
    public class SeguridadConfiguracion : IEntityTypeConfiguration<Seguridad>
    {
        public void Configure(EntityTypeBuilder<Seguridad> builder)
        {
            builder.ToTable("SEG_USUARIO");
            builder.HasKey(u => u.CodigoUsuario);

            builder.Property(p => p.CodigoUsuario).HasColumnName("COD_USUARIO").IsRequired().HasDefaultValueSql("NEWID()");
            builder.Property(p => p.IdentificadorClinica).HasColumnName("ID_CLINICA").IsRequired();
            builder.Property(p => p.NombreUsuario).HasColumnName("NOM_USUARIO").IsRequired().HasMaxLength(50);
            builder.Property(p => p.NombrePersona).HasColumnName("NOM_PERSONA").IsRequired().HasMaxLength(200);
            builder.Property(p => p.Contraseña).HasColumnName("CONTRASEÑA").IsRequired();
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO").IsRequired().HasMaxLength(1).HasDefaultValue("1");
            builder.Property(p => p.FechaRegistro).HasColumnName("FEC_REGISTRO").IsRequired().HasDefaultValueSql("GETDATE()");

            builder.HasOne(p => p.Clinica) 
           .WithMany()
           .HasForeignKey(p => p.IdentificadorClinica) 
           .HasPrincipalKey(c => c.IdentificadorClinica); 
        }
    }
}
