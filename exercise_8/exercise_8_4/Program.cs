using System.Collections.Generic;
using System.Xml;
using static System.Console;
using static exercise_8_4.GlobalStringVariables;

namespace exercise_8_4
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Initial setting
            XmlDocument document = new XmlDocument();
            XmlElement element;

            element = document.CreateElement(Elements.Person);
            document.AppendChild(element);
            element = document.CreateElement(Elements.Address);
            document.DocumentElement.AppendChild(element);
            element = document.CreateElement(Elements.Phones);
            document.DocumentElement.AppendChild(element);

            // Input
            InputData(document);

            // Output
            Clear();
            //document.Save(Out);
            document.Save("data.xml");
            ReadKey();
        }

        #region Methods

        public static void InputData(XmlDocument document)
        {
            List<string> list = new List<string>();
            XmlElement element;
            XmlAttribute attribute;
            XmlText text;

            Write("Ф.И.О.: ");
            list.Add(ReadLine());
            attribute = document.CreateAttribute(Attributes.name);
            attribute.Value = list[0];
            document.DocumentElement.SetAttributeNode(attribute);

            Write("Улица: ");
            list.Add(ReadLine());
            element = document.CreateElement(Elements.Street);
            text = document.CreateTextNode(list[1]);
            document.DocumentElement.FirstChild.AppendChild(element);
            document.DocumentElement.FirstChild.LastChild.AppendChild(text);

            Write("Номер дома: ");
            list.Add(ReadLine());
            element = document.CreateElement(Elements.HouseNumber);
            text = document.CreateTextNode(list[2]);
            document.DocumentElement.FirstChild.AppendChild(element);
            document.DocumentElement.FirstChild.LastChild.AppendChild(text);

            Write("Номер квартиры: ");
            list.Add(ReadLine());
            element = document.CreateElement(Elements.FlatNumber);
            text = document.CreateTextNode(list[3]);
            document.DocumentElement.FirstChild.AppendChild(element);
            document.DocumentElement.FirstChild.LastChild.AppendChild(text);

            Write("Мобильный телефон: ");
            list.Add(ReadLine());
            element = document.CreateElement(Elements.MobilePhone);
            text = document.CreateTextNode(list[4]);
            document.DocumentElement.LastChild.AppendChild(element);
            document.DocumentElement.LastChild.LastChild.AppendChild(text);

            Write("Домашний телефон: ");
            list.Add(ReadLine());
            element = document.CreateElement(Elements.FlatPhone);
            text = document.CreateTextNode(list[5]);
            document.DocumentElement.LastChild.AppendChild(element);
            document.DocumentElement.LastChild.LastChild.AppendChild(text);
        }

        #endregion
    }
}
