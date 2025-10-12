using CurrencyServices.CurrencyFetcher.Infrastructure.Interfaces;
using System.Xml.Serialization;

namespace CurrencyServices.CurrencyFetcher.Infrastructure.Dtos;

[XmlRoot("ValCurs")]
public class CbrResponse : ICbrResponse
{
    [XmlElement("Valute")]
    public List<Valute> Valutes { get; set; } = new List<Valute>();
}

public class Valute
{

    [XmlElement("Name")]
    public string Name { get; set; } = string.Empty;

    [XmlElement("VunitRate")]
    public string Nominal { get; set; } = string.Empty;
}
