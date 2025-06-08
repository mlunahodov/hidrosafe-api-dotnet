using System;

namespace HidroSafe.API.Models.Dtos
{
    /// <summary>
    /// DTO utilizado para entrada e saída de dados da estação.
    /// </summary>
    public class EstacaoMonitoramentoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Localizacao { get; set; } = string.Empty;
        public DateTime DataInstalacao { get; set; }
    }
}
