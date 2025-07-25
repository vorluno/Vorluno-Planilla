using Microsoft.EntityFrameworkCore;
using Planilla.Application.Interfaces;
using Planilla.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Planilla.Infrastructure.Repositories
{
    /// <summary>
    /// Implementación genérica del patrón de repositorio que encapsula la lógica básica de acceso a datos para cualquier entidad.
    /// Utiliza Entity Framework Core para interactuar con la base de datos.
    /// </summary>
    /// <typeparam name="T">El tipo de la entidad para la cual este repositorio opera. Debe ser una clase.</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Repository{T}"/>.
        /// </summary>
        /// <param name="context">El contexto de la base de datos (DbContext) que será utilizado para las operaciones de datos.</param>
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Agrega una nueva entidad a la base de datos de forma asíncrona.
        /// </summary>
        /// <param name="entity">La entidad a agregar.</param>
        /// <returns>Una tarea que representa la operación asíncrona.</returns>
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        /// <summary>
        /// Obtiene todas las entidades de un tipo específico de forma asíncrona.
        /// </summary>
        /// <returns>
        /// Una tarea que retorna una colección enumerable de todas las entidades.
        /// </returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Obtiene una entidad por su identificador único (ID) de forma asíncrona.
        /// </summary>
        /// <param name="id">El ID de la entidad a buscar.</param>
        /// <returns>
        /// Una tarea que retorna la entidad encontrada, o null si no se encuentra ninguna entidad con ese ID.
        /// </returns>
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Marca una entidad como eliminada en el contexto. 
        /// Los cambios no se persistirán en la base de datos hasta que se llame a CompleteAsync() en la Unidad de Trabajo.
        /// </summary>
        /// <param name="entity">La entidad a eliminar.</param>
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Marca una entidad como modificada en el contexto.
        /// Los cambios no se persistirán en la base de datos hasta que se llame a CompleteAsync() en la Unidad de Trabajo.
        /// </summary>
        /// <param name="entity">La entidad a actualizar.</param>
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}