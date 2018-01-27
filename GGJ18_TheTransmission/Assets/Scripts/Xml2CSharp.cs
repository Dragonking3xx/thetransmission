using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace Xml2CSharp
{
    [XmlRoot(ElementName = "option")]
    public class Option
    {
        [XmlAttribute(AttributeName = "text")]
        public string Text { get; set; }
        [XmlAttribute(AttributeName = "togetPage")]
        public string TogetPage { get; set; }
        [XmlAttribute(AttributeName = "action")]
        public string Action { get; set; }
    }

    [XmlRoot(ElementName = "options")]
    public class Options
    {
        [XmlElement(ElementName = "option")]
        public List<Option> Option { get; set; }
    }

    [XmlRoot(ElementName = "page")]
    public class Page
    {
        [XmlElement(ElementName = "text")]
        public string Text { get; set; }
        [XmlElement(ElementName = "options")]
        public Options Options { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "dialog")]
    public class Dialog
    {
        [XmlElement(ElementName = "page")]
        public List<Page> Page { get; set; }
        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "dialogs")]
    public class Dialogs
    {
        [XmlElement(ElementName = "dialog")]
        public List<Dialog> Dialog { get; set; }
    }

}
