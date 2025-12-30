using Planilla.Domain.Enums;

namespace Planilla.Application.DTOs;

public record HoraExtraDto(
    int Id,
    int EmpleadoId,
    string EmpleadoNombre,
    DateTime Fecha,
    TipoHoraExtra TipoHoraExtra,
    string TipoNombre,
    TimeSpan HoraInicio,
    TimeSpan HoraFin,
    decimal CantidadHoras,
    decimal FactorMultiplicador,
    decimal? MontoCalculado,
    bool EstaAprobada,
    string? AprobadoPor,
    string Motivo
);
