using Planilla.Application.Interfaces;
using Planilla.Application.Services;
using Planilla.Infrastructure.Repositories;
using Planilla.Web.Interop;

namespace Planilla.Web.Extensions
{
    /// <summary>
    /// Clase estática que contiene métodos de extensión para configurar los servicios de la aplicación.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Registra los servicios de la capa de aplicación e infraestructura en el contenedor de dependencias.
        /// </summary>
        /// <param name="services">La colección de servicios a la que se agregarán los registros.</param>
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            // Registra la IUnitOfWork. Cuando una clase pida una IUnitOfWork,
            // el sistema le entregará una instancia de nuestra clase UnitOfWork.
            // Se registra como 'Scoped', lo que significa que se crea una nueva instancia por cada petición HTTP.

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // REGISTRAR NUESTRO NUEVO SERVICIO
            services.AddScoped<PlanillaService>();

            // REGISTRAR NUESTRO SERVICIO DE INTEROPERABILIDAD CON JAVASCRIPT
            services.AddScoped<ReactJsInterop>();
        }
    }
}