using System;
using System.ComponentModel.DataAnnotations;
using MinhaApi.Models;

public class StatusValidoAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return new ValidationResult("StatusOperacional é obrigatório.");

        var statusStr = value.ToString();

        // Verifica se o valor existe no enum
        if (!Enum.TryParse<StatusEquipamento>(statusStr, ignoreCase: true, out _))
        {
            return new ValidationResult(
                $"StatusOperacional inválido. Valores permitidos: {string.Join(", ", Enum.GetNames(typeof(StatusEquipamento)))}");
        }

        return ValidationResult.Success;
    }
}