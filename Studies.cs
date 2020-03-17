using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cwiczenia2
{
    public class Studies
    {
        [XmlElement(elementName: "name")]
        public string Name { get; set; }

        [XmlElement(elementName: "mode")]
        public string Mode { get; set; }

        public void Create(string name, string mode)
        {

            this.Name = name;
            this.Mode = mode;
        }
    }
    
}
