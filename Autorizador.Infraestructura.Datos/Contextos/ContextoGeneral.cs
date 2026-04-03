using Autorizador.Core.Utils;
using Autorizador.Infraestructura.Datos.Configuraciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace Autorizador.Infraestructura.Datos.Contextos
{
    public class ContextoGeneral : ContextoBase<ContextoGeneral>
    {
        public ContextoGeneral(
            DbContextOptions<ContextoGeneral> options, IOptions<Configuracion> configuracion) : base(options, configuracion)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Seguridad - SG
            modelBuilder.ApplyConfiguration(new SeguridadConfiguracion());
            modelBuilder.ApplyConfiguration(new ClinicaConfiguracion());
            #endregion
        }
    }
}