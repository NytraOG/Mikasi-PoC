using Domain.Data.BaseTypes;

namespace Domain.Data.Entities;

public class Währung : BaseEntity
{
    public string Bezeichnung { get; set; } = "Euro";
    public char   Symbol      { get; set; } = '€';
    public decimal ProEuroKurs { get; set; } = 1;
}