using System.Xml.Serialization;
using Console;
using XmlLab;

// var gun = new Gun();
//
// XmlSerializer xs = new XmlSerializer(typeof(Gun));
// TextWriter tw = new StreamWriter(@"../../../../gun.xml");
// xs.Serialize(tw, gun);
//
// System.Console.WriteLine("Done");

var guns = XmlHelper.ProcessXml();
foreach (var gun in guns)
{
    System.Console.WriteLine(gun.Id);
}

System.Console.WriteLine("\nXML validation result:");
XmlHelper.ValidateXml();

System.Console.WriteLine("\nXML -> JSON:");
XmlHelper.XmlToJson();
