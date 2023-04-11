using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Atos.Core.Abstractions
{
    /// <summary>
    /// Especificación del contrato para la administración de servicios personalizados en el SDK Atos.Microservices
    /// </summary>
    public interface IStartupServices
    {
        /// <summary>
        /// Este metodo es invocado por el SDK Atos.Microservices en el momento de iniciar la aplicación para registrar servicios personalizados.
        /// </summary>
        /// <param name="services">Provee acceso al contenedor de dependencias de .net core</param>
        /// <param name="configuration">Provee acceso a las diferentes fuentes de configuración</param>
        void Initialize(IServiceCollection services, IConfiguration configuration);
    }
}
