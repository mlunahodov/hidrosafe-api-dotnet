using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HidroSafe.API.Models
{
    /// <summary>
    /// Representa uma estação de monitoramento responsável por registrar leituras de distância da água.
    /// </summary>
    [Table("ESTACOES")]
    public class EstacaoMonitoramento
    {
        /// <summary>
        /// Construtor que inicializa listas e strings obrigatórias.
        /// </summary>
        public EstacaoMonitoramento()
        {
            Nome = string.Empty;
            Localizacao = string.Empty;
            Leituras = new List<LeituraDistancia>();
        }

        /// <summary>
        /// Identificador único da estação.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Nome da estação.
        /// </summary>
        [Required(ErrorMessage = "O nome da estação é obrigatório.")]
        public string Nome { get; set; }

        /// <summary>
        /// Localização da estação.
        /// </summary>
        [Required(ErrorMessage = "A localização é obrigatória.")]
        public string Localizacao { get; set; }

        /// <summary>
        /// Data de instalação da estação.
        /// </summary>
        [Required(ErrorMessage = "A data de instalação é obrigatória.")]
        public DateTime DataInstalacao { get; set; }

        /// <summary>
        /// Leituras associadas à estação.
        /// </summary>
        public ICollection<LeituraDistancia> Leituras { get; set; }
    }
}
