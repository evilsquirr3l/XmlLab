using System.Xml;
using System.Xml.Schema;
using Newtonsoft.Json;
using XmlLab;

namespace Console;

public static class XmlHelper
{
    public static void XmlToJson()
    {
        var xDoc = new XmlDocument();
        xDoc.Load("guns.xml");
        var xRoot = xDoc.DocumentElement;

        var childNodes = xRoot!.SelectNodes("*")!;
        foreach (XmlNode n in childNodes)
            System.Console.WriteLine(JsonConvert.SerializeXmlNode(n, Newtonsoft.Json.Formatting.Indented, false));
    }

    public static Gun[] ProcessXml()
    {
        var guns = new List<Gun>();

        var xDoc = new XmlDocument();
        xDoc.Load("guns.xml");
        var xRoot = xDoc.DocumentElement;

        var childNodes = xRoot!.SelectNodes("*")!;
        foreach (XmlNode n in childNodes)
        {
            var gun = new Gun()
            {
                Id = n.SelectSingleNode("@Id")?.Value ?? "",
                Handy = Enum.Parse<Handy>(n.SelectSingleNode("Handy")?.InnerText ?? "Unknown"),
                Model = new Model()
                {
                    Id = Convert.ToInt32(n.SelectSingleNode("//Id")?.InnerText),
                    Name = n.SelectSingleNode("//Name")?.Value ?? "",
                },
                Origin = new Origin()
                {
                    ProductionYear = Convert.ToInt32(n.SelectSingleNode("//ProductionYear")?.InnerText),
                    Country = n.SelectSingleNode("//Country")?.Value ?? "",
                },
                Ttc = new TTC()
                {
                    HasClip = Convert.ToBoolean(n.SelectSingleNode("//HasClip")?.InnerText),
                    HasOptics = Convert.ToBoolean(n.SelectSingleNode("//HasOptics")?.InnerText),
                    Range = Enum.Parse<HitRange>(n.SelectSingleNode("//Range")?.InnerText ?? "???"),
                    ShootingRange = Convert.ToInt32(n.SelectSingleNode("//ShootingRange")?.InnerText),
                }
            };

            guns.Add(gun);
        }

        guns.Sort();

        return guns.ToArray();
    }

    public static void ValidateXml()
    {
        var schema = new XmlSchemaSet();
        schema.Add("", "gun.xsd");

        var xmlDoc = new XmlDocument();
        xmlDoc.Load("invalid.xml");
        xmlDoc.Schemas.Add(schema);
        xmlDoc.Validate(ValidationEventHandler!);
    }

    private static void ValidationEventHandler(object sender, ValidationEventArgs e)
    {
        if (e.Severity == XmlSeverityType.Error)
            System.Console.WriteLine(e.Message);
    }
}