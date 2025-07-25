using Planilla.Application.Interfaces;
using Planilla.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planilla.Application.Services
{
    /// <summary>
    /// Proporciona la lógica de negocio central para las operaciones de la planilla.
    /// Orquesta el uso de repositorios a través de la Unidad de Trabajo para ejecutar los casos de uso.
    /// </summary>
    public class PlanillaService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="PlanillaService"/>.
        /// </summary>
        /// <param name="unitOfWork">La unidad de trabajo que proporciona acceso a los repositorios.</param>
        public PlanillaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtiene todos los empleados registrados en el sistema.
        /// </summary>
        /// <returns>Una colección de entidades Empleado.</returns>
        public async Task<IEnumerable<Empleado>> GetAllEmployeesAsync()
        {
            return await _unitOfWork.Empleados.GetAllAsync();
        }

        // Aquí irían otros métodos de negocio complejos, como:
        // public async Task<ReciboDeSueldo> CalcularYGuardarReciboSueldoAsync(int empleadoId, ...)
    }
}