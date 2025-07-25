using Microsoft.AspNetCore.Identity;

namespace Planilla.Domain.Entities;

// La palabra 'public' es crucial para que otros proyectos puedan ver esta clase.
public class AppUser : IdentityUser
{
    // Aquí puedes añadir propiedades personalizadas a tus usuarios en el futuro.
    // Por ejemplo:
    public string? NombreCompleto { get; set; }
}