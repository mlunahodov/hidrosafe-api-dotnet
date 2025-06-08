using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HidroSafe.API.Models
{
    /// <summary>
    /// Representa uma leitura de distância da água em uma estação de monitoramento.
    /// </summary>
    [Table("LEITURAS")]
    public class LeituraDistancia
    {
        /// <summary>
        /// Identificador único da leitura.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Data e hora em que a leitura foi registrada.
        /// </summary>
        [Required(ErrorMessage = "A data e hora da leitura são obrigatórias.")]
        public DateTime DataHora { get; set; }

        /// <summary>
        /// Distância medida da água até a margem (em centímetros).
        /// </summary>
        [Required(ErrorMessage = "A distância da margem é obrigatória.")]
        public double DistanciaMargemCm { get; set; }

        /// <summary>
        /// Identificador da estação de monitoramento associada.
        /// </summary>
        [Required(ErrorMessage = "O ID da estação é obrigatório.")]
        public int EstacaoMonitoramentoId { get; set; }

        /// <summary>
        /// Estação de monitoramento relacionada à leitura. (Não obrigatória no POST)
        /// </summary>
        [ForeignKey("EstacaoMonitoramentoId")]
        public EstacaoMonitoramento? EstacaoMonitoramento { get; set; }

        /// <summary>
        /// Status do nível de água com base na distância da margem.
        /// </summary>
        [NotMapped]
        public string Status
        {
            get
            {
                if (DistanciaMargemCm > 60) return "Normal";
                if (DistanciaMargemCm > 40) return "Atenção";
                if (DistanciaMargemCm > 20) return "Alerta";
                return "Crítico";
            }
        }
    }
}
