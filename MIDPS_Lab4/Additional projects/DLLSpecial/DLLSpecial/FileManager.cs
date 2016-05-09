using System;
using IniParser;
using IniParser.Model;
using System.Xml;
namespace DLLSpecial
{
    class FileManager
    {
        public static string xmlConnectionString()
        {
            XmlDocument document = new XmlDocument();
            document.Load("config.xml");
            return document.SelectSingleNode("/Data/ConnectionString").InnerText;
        }

        public static uint getLastID()
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("file1.ini");
            uint temp = 0;
            uint.TryParse(data["ID"]["lastid"], out temp);
            return temp;
        }

        public static IniData getData()
        {
            var parser = new FileIniDataParser();
            IniData data = new IniData();
            try
            {
                data = parser.ReadFile("config.ini");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return data;
        }

        public static void writeID(uint id)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile("file1.ini");
            data["ID"]["lastid"] = id.ToString();
            parser.WriteFile("file1.ini", data);
        }
    }
}
