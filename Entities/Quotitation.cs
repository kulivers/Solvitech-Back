using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Entities
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [SerializableAttribute]
    [DesignerCategory("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class ValCurs
    {
        private ValCursValute[] valuteField;

        private string dateField;

        private string nameField;

        /// <remarks/>
        [XmlElementAttribute("Valute")]
        public ValCursValute[] Valute
        {
            get { return valuteField; }
            set { valuteField = value; }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string Date
        {
            get { return dateField; }
            set { dateField = value; }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string name
        {
            get { return nameField; }
            set { nameField = value; }
        }
    }

    /// <remarks/>
    [SerializableAttribute]
    [DesignerCategory("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public class ValCursValute
    {
        private ushort numCodeField;

        private string charCodeField;

        private ushort nominalField;

        private string nameField;

        private string valueField;

        private string idField;

        /// <remarks/>
        public ushort NumCode
        {
            get { return numCodeField; }
            set { numCodeField = value; }
        }

        /// <remarks/>
        public string CharCode
        {
            get { return charCodeField; }
            set { charCodeField = value; }
        }

        /// <remarks/>
        public ushort Nominal
        {
            get { return nominalField; }
            set { nominalField = value; }
        }

        /// <remarks/>
        public string Name
        {
            get { return nameField; }
            set { nameField = value; }
        }

        /// <remarks/>
        public string Value
        {
            get { return valueField; }
            set { valueField = value; }
        }

        /// <remarks/>
        [XmlAttributeAttribute]
        public string ID
        {
            get { return idField; }
            set { idField = value; }
        }
    }
}