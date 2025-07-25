using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace Planilla.Web.Interop
{
    /// <summary>
    /// Servicio para encapsular la interoperabilidad entre Blazor (C#) y JavaScript (React).
    /// </summary>
    public class ReactJsInterop : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ReactJsInterop"/>.
        /// </summary>
        /// <param name="jsRuntime">El servicio de Blazor para interactuar con JavaScript.</param>
        public ReactJsInterop(IJSRuntime jsRuntime)
        {
            // Creamos una tarea perezosa para importar nuestro script de React como un módulo.
            // Esto es eficiente porque solo se carga una vez, la primera vez que se necesita.
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "/react/react-app.js").AsTask());
        }

        /// <summary>
        /// Invoca la función global 'renderReactApp' en JavaScript para montar la aplicación de React.
        /// </summary>
        /// <param name="containerId">El ID del elemento div donde se renderizará React.</param>
        /// <param name="initialData">Los datos iniciales que se pasarán a la aplicación React como props.</param>
        /// <param name="dotNetHelper">Una referencia a un objeto .NET para permitir la comunicación de React a Blazor.</param>
        public async ValueTask RenderApp(string containerId, object? initialData, object? dotNetHelper = null)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("renderReactApp", containerId, initialData, dotNetHelper);
        }

        /// <summary>
        /// Libera la referencia al módulo de JavaScript para evitar fugas de memoria.
        /// </summary>
        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}