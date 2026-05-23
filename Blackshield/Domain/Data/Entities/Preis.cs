namespace Domain.Data.Entities;

public readonly record struct Preis(decimal Betrag, string Währung = "EUR");