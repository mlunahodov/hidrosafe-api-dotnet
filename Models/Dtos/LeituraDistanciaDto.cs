using System;

namespace HidroSafe.API.Models.Dtos
{
    /// <summary>
    /// DTO para leitura de distância com nome da estação.
    /// </summary>
    public class LeituraDistanciaDto
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public double DistanciaMargemCm { get; set; }
        public int EstacaoMonitoramentoId { get; set; }
        public string NomeEstacao { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
