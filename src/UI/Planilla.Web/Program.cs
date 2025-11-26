using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Planilla.Web.Components;
using Planilla.Web.Components.Account;
using Planilla.Infrastructure.Data;
using Planilla.Domain.Entities;
using Planilla.Web.Extensions;
using Planilla.Application.Mappings;

var builder = WebApplication.CreateBuilder(args);

// --- INICIO DE LA CONFIGURACI�N DE SERVICIOS ---


// 1. LEER LA CADENA DE CONEXI�N
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// 2. REGISTRAR EL DBCONTEXT PARA INYECCI�N DE DEPENDENCIAS
//    Le decimos a la aplicaci�n c�mo crear instancias de nuestro ApplicationDbContext,
//    configur�ndolo para que use SQL Server con la cadena de conexi�n.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. CONFIGURAR ASP.NET CORE IDENTITY
//    Configura el sistema de usuarios y roles, usando nuestro ApplicationDbContext para almacenar los datos
//    y nuestra clase AppUser como el modelo de usuario.
builder.Services.AddIdentity<AppUser, IdentityRole>(options => {
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 4. <<<---  AUTOMAPPER ---<<<
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// 5. REGISTRAR SERVICIOS DE LA APLICACI�N (UnitOfWork, etc.)
builder.Services.ConfigureApplicationServices();

// --- FIN DE NUESTRA CONFIGURACI�N PRINCIPAL ---


// Servicios est�ndar de Blazor y de la plantilla de Identity
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSingleton<IEmailSender<AppUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

// --- CONFIGURACI�N DEL PIPELINE DE PETICIONES HTTP (MIDDLEWARE) ---
// El orden aqu� es importante.

if (app.Environment.IsDevelopment())
{
    // En desarrollo, muestra p�ginas de error detalladas para la base de datos.
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseMigrationsEndPoint();
}
else
{
    // En producci�n, usa un manejador de errores gen�rico y fuerza el uso de HTTPS.
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Permite servir archivos est�ticos como CSS, JS e im�genes desde la carpeta wwwroot.
app.UseAntiforgery(); // Middleware de seguridad contra ataques CSRF.

// Activa la autorizaci�n para que los atributos [Authorize] en los controladores funcionen.
app.UseAuthorization();

// Mapea los componentes de la aplicaci�n y los endpoints de Identity.
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

// <<<---  PARA ACTIVAR LOS CONTROLADORES DE API ---<<<
app.MapControllers();

// Inicia y ejecuta la aplicaci�n.
app.Run();