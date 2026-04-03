using Autorizador.Core.Utils;
using Autorizador.Infraestructura.Datos.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;


namespace Autorizador.Infraestructura.Datos.Contextos
{
    public class ContextoBase<TContexto> : DbContext, IContextoBase where TContexto : class
    {
        /// <summary>
        /// Instancia de la interfaz IBitacora
        /// </summary>

        private readonly DbContextOptions<ContextoGeneral> _options;
        private readonly IOptions<Configuracion> _configuracion;

        /// <summary>
        /// Constructor estatico, establece la inicialización de los elementos de la base de datos.
        /// </summary>
        public ContextoBase(DbContextOptions<ContextoGeneral> options, IOptions<Configuracion> configuracion) : base(options)
        {
            _options = options;
            _configuracion = configuracion;
        }

        /// <summary>
        /// Configuracion de la cadena de conexion
        /// </summary>
        /// <param name="optionsBuilder">DbContextOptionsBuilder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(ObtenerCadenaConexion(), o => o.UseCompatibilityLevel(120));
            optionsBuilder.EnableDetailedErrors(true);
        }

        /// <summary>
        /// Método para establecer la conexion con la base de datos
        /// </summary>
        /// <returns>Datos de la conexion</returns>
        private string ObtenerCadenaConexion()
        {
            var servidor = _configuracion.Value.BD_SERVIDOR;
            var catalogo = _configuracion.Value.BD_CATALOGO;
            var esSeguridadIntegrada = _configuracion.Value.USAR_SEGURIDAD_INTEGRADA_BD;

            var datosConexion = new SqlConnectionStringBuilder
            {
                DataSource = servidor,
                InitialCatalog = catalogo,
                ApplicationName = "AutorizadorCentralizado v1",
                IntegratedSecurity = _configuracion.Value.USAR_SEGURIDAD_INTEGRADA_BD,
                TrustServerCertificate = true
            };

            if (!esSeguridadIntegrada)
            {
                datosConexion.UserID = _configuracion.Value.BD_USUARIO;
                datosConexion.Password = _configuracion.Value.BD_CLAVE;
            }

            return datosConexion.ConnectionString;
        }

        /// <summary>
        /// Método que obtiene el conjunto de datos de una entidad.
        /// </summary>
        /// <typeparam name="T">Tipo de conjunto.</typeparam>
        /// <returns>Conjunto de datos de una entidad</returns>
        public DbSet<T> Establecer<T>() where T : class
        {
            return Set<T>();
        }

        /// <summary>
        /// Método para guardar los cambios a la base de datos.
        /// </summary>
        public void GuardarCambios()
        {
            try
            {
                SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var entidadesInvolucradas = ex.Entries.Aggregate(string.Empty,
                    (current, entidad) => current + (entidad.Entity.GetType() + ","));



                throw new Exception("Error al guardar cambios en BBDD. Problemas de concurrencia.", ex);
            }
            catch (DbUpdateException ex)
            {
                var mensaje = ex.Entries.Aggregate(string.Empty,
                    (current, entidad) => current + ("Entidad de tipo " + entidad.Entity.GetType().Name +
                                                     " en estado " + entidad.State +
                                                     " tiene los siguientes errores de validación: "));

                throw new Exception("Error al guardar cambios en BBDD. " + mensaje, ex);
            }
        }
    }
}
