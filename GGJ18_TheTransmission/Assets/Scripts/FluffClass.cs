using System;
using System.Xml.Serialization;
using System.Collections.Generic;
namespace FluffClass
{
    [XmlRoot(ElementName = "dialog")]
    public class Dialog
    {
        [XmlElement(ElementName = "text")]
        public string Text { get; set; }
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


