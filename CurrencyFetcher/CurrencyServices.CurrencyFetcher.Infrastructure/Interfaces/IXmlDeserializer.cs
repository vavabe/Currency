namespace CurrencyServices.CurrencyFetcher.Infrastructure.Interfaces;

public interface IXmlDeserializer
{
    ICbrResponse Deserialize(Stream xmlStream);
}
