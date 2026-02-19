using System;
using System.ComponentModel.DataAnnotations;

namespace MonitoramentoEquipamentosPesados.Models
{
    public class Equipamento
    {
        public int Id { get; set; }

        [Required]
        public string Codigo { get; set; } = string.Empty;

        [Required]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        public string Modelo { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal Horimetro { get; set; }

        [Required]
        public string StatusOperacional { get; set; } = "Operacional";

        public DateTime? DataAquisicao { get; set; }

        public string? LocalizacaoAtual { get; set; }
    }
}
