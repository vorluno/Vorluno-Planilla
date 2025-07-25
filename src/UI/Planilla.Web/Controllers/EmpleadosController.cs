using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Planilla.Application.DTOs;
using Planilla.Application.Interfaces;
using Planilla.Domain.Entities;

namespace Planilla.Web.Controllers
{
    /// <summary>
    /// Controlador de API para gestionar las operaciones CRUD de los empleados.
    /// </summary>
    [Authorize] // Protegerá todo el controlador una vez configuremos la seguridad.
    [ApiController]
    [Route("api/[controller]")] // La ruta de acceso será /api/empleados
    public class EmpleadosController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="EmpleadosController"/>.
        /// </summary>
        /// <param name="unitOfWork">La unidad de trabajo para el acceso a datos.</param>
        /// <param name="mapper">El servicio de mapeo de objetos (AutoMapper).</param>
        public EmpleadosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Obtiene una lista de todos los empleados.
        /// </summary>
        /// <returns>Una lista de empleados en formato DTO.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var empleados = await _unitOfWork.Empleados.GetAllAsync();
            var empleadosDto = _mapper.Map<IEnumerable<EmpleadoVerDto>>(empleados);
            return Ok(empleadosDto);
        }

        /// <summary>
        /// Obtiene un empleado específico por su ID.
        /// </summary>
        /// <param name="id">El ID del empleado.</param>
        /// <returns>El empleado encontrado o un error 404 si no existe.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
            if (empleado == null)
            {
                return NotFound(); // Retorna un 404 Not Found si no existe
            }
            var empleadoDto = _mapper.Map<EmpleadoVerDto>(empleado);
            return Ok(empleadoDto);
        }

        /// <summary>
        /// Crea un nuevo empleado.
        /// </summary>
        /// <param name="empleadoDto">Los datos del nuevo empleado.</param>
        /// <returns>El nuevo empleado creado.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")] // En el futuro, solo los administradores podrán crear empleados.
        public async Task<IActionResult> Create(EmpleadoCrearDto empleadoDto)
        {
            var empleado = _mapper.Map<Empleado>(empleadoDto);
            empleado.FechaContratacion = DateTime.UtcNow; // Lógica de negocio simple

            await _unitOfWork.Empleados.AddAsync(empleado);
            await _unitOfWork.CompleteAsync(); // Guarda los cambios en la BD

            var empleadoCreadoDto = _mapper.Map<EmpleadoVerDto>(empleado);

            // Devuelve una respuesta 201 Created con la ubicación del nuevo recurso
            return CreatedAtAction(nameof(GetById), new { id = empleado.Id }, empleadoCreadoDto);
        }
    }
}