using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace cwiczenia2
{

    
    public class Student
    {
        [XmlElement(elementName: "fname")]
        public string Fname { get; set; }


        [XmlElement(elementName: "lname")]
        public string Lname { get; set; }


        [XmlElement(elementName: "birthdate")]
        public string Birthdate { get; set; }


        [XmlElement(elementName: "email")]
        public string Email { get; set; }


        [XmlElement(elementName: "mothersName")]
        public string MothersName { get; set; }


        [XmlElement(elementName: "fathersName")]
        public string FathersName { get; set; }


        [XmlElement(elementName: "studies")]
        public Studies StudiesMode { get; set; }


        [XmlAttribute(attributeName: "indexNumber")]
        public int IndexNumber { get; set; }

       
        [XmlIgnore]
        public bool IsCorrect { get; set; }
        
        [XmlIgnore]
        public string Line { get; set; }
        public void Create()
        {
            string[] splited = this.Line.Split(",");
            this.Fname = splited[0];
            this.Lname = splited[1];
            this.IndexNumber = Int32.Parse(splited[4]);
            this.Birthdate = splited[5];
            this.Email = splited[6];
            this.MothersName = splited[7];
            this.FathersName = splited[8];
            this.IsCorrect = Check(splited);
            this.StudiesMode = new Studies();
            StudiesMode.Create(splited[2], splited[3]);
        }
        public override string ToString()
        {
            return Line;
        }
        private bool Check(string[] tab)
        {
            bool check = true;
            foreach (string s in tab)
            {
                if (s.Length == 0)
                    check = false;
            }
            return check;
        }
    }
}