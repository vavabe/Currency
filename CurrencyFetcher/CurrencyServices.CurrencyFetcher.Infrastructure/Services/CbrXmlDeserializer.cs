using CurrencyServices.CurrencyFetcher.Infrastructure.Dtos;
using CurrencyServices.CurrencyFetcher.Infrastructure.Interfaces;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace CurrencyServices.CurrencyFetcher.Infrastructure.Services;

internal class CbrXmlDeserializer : IXmlDeserializer
{
    public ICbrResponse Deserialize(Stream xmlStream)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using (var reader = new StreamReader(xmlStream, Encoding.GetEncoding("windows-1251")))
        using (var xmlReader = new XmlTextReader(reader))
        {
            var serializer = new XmlSerializer(typeof(CbrResponse));
            return (CbrResponse)serializer.Deserialize(xmlReader)!;
        }
    }
}
