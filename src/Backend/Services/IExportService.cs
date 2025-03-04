using System.Xml;
using System.Xml.Serialization;

namespace Backend.Services;

public interface IExportService
{
    MemoryStream ExportToXml<T>(T data, string rootElementName = "Items");
}