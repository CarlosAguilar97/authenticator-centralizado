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
    public class ClinicaConfiguracion : IEntityTypeConfiguration<Clinica>
    {
        public void Configure(EntityTypeBuilder<Clinica> builder)
        {
            builder.ToTable("MAE_CLINICA");
            builder.HasKey(c => c.IdentificadorClinica);

            builder.Property(p => p.IdentificadorClinica).HasColumnName("ID_CLINICA").IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.NombreClinica).HasColumnName("NOM_CLINICA").IsRequired().HasMaxLength(150);
            builder.Property(p => p.IndicadorEstado).HasColumnName("IND_ESTADO").IsRequired().HasMaxLength(1).HasDefaultValue("1");
        }
    }
}
