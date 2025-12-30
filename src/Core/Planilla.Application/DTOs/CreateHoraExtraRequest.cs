using Planilla.Domain.Enums;

namespace Planilla.Application.DTOs;

public record CreateHoraExtraRequest(
    int EmpleadoId,
    DateTime Fecha,
    TipoHoraExtra TipoHoraExtra,
    TimeSpan HoraInicio,
    TimeSpan HoraFin,
    string Motivo
);
