using Autorizador.Core.Repositorios;
using Autorizador.Core.Servicios;
using Autorizador.Infraestructura.Datos.Contextos;
using Autorizador.Infraestructura.Datos.Repositorios;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autorizador.Infraestructura.Contenedor
{
    public static class ConfigurarIoC
    {
        public static IServiceCollection AddConfiguracionInfraestructura(this IServiceCollection services)
        {
            services.AddDbContext<ContextoGeneral>();
            services.AddScoped<Sesion>();
            services.AddScoped<IContextoAplicacion>(x => x.GetRequiredService<Sesion>());
            services.AddScoped<IRepositorioConsultaGeneral, RepositorioConsultaGeneral>();
            services.AddScoped<IRepositorioOperacionGeneral, RepositorioConsultaGeneral>();

            return services;
        }
    }
}
