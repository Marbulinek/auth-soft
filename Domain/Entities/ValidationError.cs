namespace Domain.Entities;

public record ValidationError(string propertyName, string errorMessage);