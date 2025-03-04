using System.Xml;
using System.Xml.Serialization;

namespace Backend.Services;
public class ExportService : IExportService
{
    public MemoryStream ExportToXml<T>(T data, string rootElementName = "Items")
    {
        var memoryStream = new MemoryStream();
        var xmlWriterSettings = new XmlWriterSettings
        {
            Encoding = System.Text.Encoding.UTF8,
            Indent = true,
            IndentChars = "  ",
            NewLineHandling = NewLineHandling.Replace
        };

        // Creiamo un XmlRoot attribute per personalizzare il nome dell'elemento root
        var attributeOverrides = new XmlAttributeOverrides();
        var attributes = new XmlAttributes();
        attributes.XmlRoot = new XmlRootAttribute(rootElementName);
        attributeOverrides.Add(typeof(T), attributes);

        var serializer = new XmlSerializer(typeof(T), attributeOverrides);

        using (var xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings))
        {
            // Aggiungiamo l'XML declaration
            xmlWriter.WriteStartDocument();
            
            // Serializziamo i dati
            serializer.Serialize(xmlWriter, data);
        }

        memoryStream.Position = 0;
        return memoryStream;
    }
}