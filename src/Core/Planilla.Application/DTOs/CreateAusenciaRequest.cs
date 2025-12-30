using Planilla.Domain.Enums;

namespace Planilla.Application.DTOs;

public record CreateAusenciaRequest(
    int EmpleadoId,
    TipoAusencia TipoAusencia,
    DateTime FechaInicio,
    DateTime FechaFin,
    string Motivo,
    bool TieneJustificacion,
    string? DocumentoReferencia
);
