# Planilla - Sistema de Gestión de Nómina para Panamá

Sistema completo de gestión de nómina empresarial con cumplimiento total de las regulaciones laborales de Panamá (CSS, Seguro Educativo, ISR). Desarrollado con Clean Architecture y las mejores prácticas de .NET.

## Características Principales

- **Gestión de Empleados**: CRUD completo con información laboral y tributaria
- **Cálculo de Nómina**: Motor de cálculo preciso que cumple con regulaciones panameñas
  - Caja de Seguro Social (CSS) con topes escalonados según Ley 462
  - Seguro Educativo sin límite máximo
  - Impuesto Sobre la Renta (ISR) con brackets progresivos 2025
  - Riesgo Profesional por categoría de trabajo
- **Gestión de Conceptos**:
  - Horas extra con diferentes multiplicadores
  - Anticipos y préstamos con amortización automática
  - Deducciones fijas y variables
  - Ausencias y vacaciones
- **Workflow de Nómina**: Estado de nómina con validaciones (Draft → Calculated → Approved → Paid)
- **Reportes**: Generación de comprobantes de pago, reportes CSS, ISR y más
- **Frontend Moderno**: SPA React 19 con Tailwind CSS

## Stack Técnico

### Backend
- **.NET 9** - Framework principal
- **ASP.NET Core** - Web API RESTful
- **Entity Framework Core 9** - ORM para SQL Server
- **ASP.NET Core Identity** - Autenticación y autorización
- **xUnit + FluentAssertions + Moq** - Testing

### Frontend
- **React 19** - Biblioteca UI
- **Vite** - Build tool y dev server
- **Tailwind CSS** - Framework CSS utility-first
- **Axios** - Cliente HTTP

### Base de Datos
- **SQL Server** - Base de datos principal
- **Migraciones EF Core** - Control de versiones de DB

## Arquitectura

El proyecto sigue los principios de **Clean Architecture**:

```
Planilla/
├── src/
│   ├── Core/
│   │   ├── Planilla.Domain/           # Entidades, enums, value objects
│   │   └── Planilla.Application/      # Servicios, DTOs, interfaces, lógica de negocio
│   ├── Infrastructure/
│   │   └── Planilla.Infrastructure/   # EF Core, repositorios, servicios externos
│   └── UI/
│       └── Planilla.Web/              # API Controllers, Program.cs, React SPA
│           └── ClientApp/             # Aplicación React
├── tests/                             # Tests unitarios e integración
└── docs/                              # Documentación del proyecto
```

### Capas y Responsabilidades

- **Domain**: Entidades del negocio, enums, objetos de valor (sin dependencias externas)
- **Application**: Servicios de aplicación, DTOs, interfaces, validaciones, lógica de negocio
- **Infrastructure**: Implementación de repositorios, EF Core, servicios externos, acceso a datos
- **Web**: Controllers API, configuración ASP.NET, hosting de SPA React

## Requisitos Previos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js 18+](https://nodejs.org/) (para el frontend React)
- [SQL Server](https://www.microsoft.com/sql-server) (LocalDB, Express o superior)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)

## Instalación y Configuración

### 1. Clonar el repositorio

```bash
git clone https://github.com/swlarot/Planilla.git
cd Planilla
```

### 2. Configurar la base de datos

Actualiza la cadena de conexión en `src/UI/Planilla.Web/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=PlanillaDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 3. Aplicar migraciones

```bash
cd src/UI/Planilla.Web
dotnet ef database update --project ../Infrastructure/Planilla.Infrastructure
```

### 4. Instalar dependencias del frontend

```bash
cd ClientApp
npm install
```

### 5. Ejecutar el proyecto

**Backend (API):**
```bash
# Desde src/UI/Planilla.Web
dotnet run
```

**Frontend (desarrollo):**
```bash
# Desde src/UI/Planilla.Web/ClientApp
npm run dev
```

La aplicación estará disponible en:
- API: `https://localhost:7105`
- Frontend: `http://localhost:5173`

## Uso

### Desarrollo

```bash
# Build completo
dotnet build Planilla.sln

# Ejecutar tests
dotnet test

# Crear nueva migración
dotnet ef migrations add NombreMigracion --project src/Infrastructure/Planilla.Infrastructure --startup-project src/UI/Planilla.Web

# Build del frontend para producción
cd src/UI/Planilla.Web/ClientApp
npm run build
```

### Endpoints API Principales

- `GET/POST/PUT/DELETE /api/empleados` - Gestión de empleados
- `GET/POST /api/departamentos` - Gestión de departamentos
- `GET/POST /api/posiciones` - Gestión de posiciones
- `POST /api/payroll/calculate` - Calcular nómina
- `GET /api/payroll/{id}/detail` - Detalle de nómina calculada
- `POST /api/horasextra` - Registrar horas extra
- `POST /api/anticipos` - Crear anticipos
- `POST /api/prestamos` - Crear préstamos
- `POST /api/vacaciones` - Solicitar vacaciones

## Convenciones de Desarrollo

### Reglas DURAS (NUNCA romper)

1. **NUNCA hardcodear tasas/montos** - Todo viene de `PayrollTaxConfiguration`
2. **NUNCA fallbacks silenciosos** - Si falta config → `throw InvalidOperationException`
3. **NUNCA borrar datos** - Usar soft deletes con `IsActive`/`DeletedAt`
4. **SIEMPRE auditar** - Campos `CreatedBy`, `CreatedAt`, `ModifiedBy`, `ModifiedAt`
5. **SIEMPRE transacciones** - Operaciones multi-tabla dentro de `UnitOfWork`
6. **DbContext SIEMPRE en Infrastructure** - Nunca en Domain o Application

### Servicios de Cálculo

- Usar `IPayrollConfigProvider` para obtener configuración (NO repositorios directos)
- Usar `RoundingPolicy.CalculatePercentage()` para cálculos monetarios precisos
- Prefijo `Portable` para servicios portados de sistemas legacy

### DTOs y Naming

- Sufijo `Dto` para DTOs de transferencia (ej: `EmpleadoDto`)
- Sufijo `Request` para DTOs de creación/actualización (ej: `CreateEmpleadoRequest`)
- Sufijo `Result` para resultados de cálculos complejos (ej: `PayrollCalculationResult`)

### Tests

- Patrón: `{Método}__{Escenario}__Returns{Resultado}`
- Ejemplo: `CalculateEmployeeCss__TopeEstandar__ReturnsCorrectAmount`
- Usar `MockPayrollConfigProvider` para aislar tests unitarios

## Contexto de Panamá

### Regulaciones Implementadas

- **CSS (Caja de Seguro Social)**: Topes escalonados según Ley 462
  - Hasta B/. 1,500: 9.75% empleado, 12.25% empleador
  - Entre B/. 1,500 - 2,000: solo empleador
  - Entre B/. 2,000 - 2,500: solo empleador
- **Seguro Educativo**: 1.25% empleado, 1.50% empleador (SIN tope)
- **ISR**: Impuesto progresivo anual con deducción por dependientes
- **Riesgo Profesional**: 0.56% (bajo), 2.50% (medio), 5.39% (alto)

### Formatos

- **Moneda**: USD (B/.), separador miles = coma, decimales = punto (1,234.56)
- **Fecha**: dd/MM/yyyy

## Estado del Proyecto

### Implementado

- [x] Arquitectura Clean Architecture completa
- [x] CRUD de empleados (API + React UI)
- [x] Gestión de departamentos y posiciones
- [x] Configuración de nómina con seeds
- [x] Servicios de cálculo portables (CSS, Seguro Educativo, ISR)
- [x] Entidades de workflow de nómina
- [x] Conceptos de nómina (horas extra, anticipos, préstamos, vacaciones, ausencias)
- [x] Migraciones EF Core completas
- [x] Frontend React con Tailwind CSS

### En Desarrollo

- [ ] Tests unitarios completos (39 tests planeados)
- [ ] Endpoints de workflow de nómina
- [ ] Reportes y comprobantes PDF
- [ ] UI de gestión de conceptos
- [ ] Autenticación con Identity (UI)
- [ ] Control de concurrencia con `RowVersion`
- [ ] Auditoría completa de cambios

### Roadmap

- [ ] Multi-tenant (soporte para múltiples empresas)
- [ ] Integración con bancos (ACH)
- [ ] Portal de empleados (self-service)
- [ ] Notificaciones por email
- [ ] Exportación a formatos contables
- [ ] Dashboard de analytics

## Documentación Adicional

- [CLAUDE.md](./CLAUDE.md) - Guía para desarrollo con Claude Code CLI
- [docs/PHASES.md](./docs/PHASES.md) - Plan de implementación por fases
- [docs/CORE360_EXTRACTION.md](./docs/CORE360_EXTRACTION.md) - Documentación de referencia del sistema legacy

## Contribuir

1. Fork el proyecto
2. Crea un branch para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add: amazing feature'`)
4. Push al branch (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## Licencia

Este proyecto es privado y propietario.

## Contacto

Proyecto desarrollado por SWLAROT

GitHub: [@swlarot](https://github.com/swlarot)

---

**Nota**: Este sistema maneja información sensible de nómina. Asegúrate de seguir las mejores prácticas de seguridad y cumplimiento de datos personales según la Ley 81 de Panamá.
