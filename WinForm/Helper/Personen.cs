using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WinForm
{
    [Serializable()]
    [System.Xml.Serialization.XmlRoot("Personen")]
    public class Personen
    {
        //[XmlArrayItem("Person", typeof(Person))]
        [XmlElement("Person")]

        public DevKotCollection<Person> Person { set; get; }

        public Personen()
        {
            this.Person = new DevKotCollection<Person>();
        }

    }

    [Serializable()]
    public class Person
    {
        public static EventHandler<DevKotCollectionEventArgs> ItemChanged;
        public Person()
        {

        }

        protected virtual void OnAddItemEvent(DevKotCollectionEventArgs e)
        {
            EventHandler<DevKotCollectionEventArgs> handler = ItemChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        [System.Xml.Serialization.XmlElement("FirstName")]
        private string firstName;

        [System.Xml.Serialization.XmlElement("LastName")]
        public string Lastname { get; set; }

        [System.Xml.Serialization.XmlElement("Street")]
        public string Adress { get; set; }

        [System.Xml.Serialization.XmlElement("PhoneNumber")]
        public string Phone { get; set; }

        [System.Xml.Serialization.XmlElement("Email")]
        public string Email { get; set; }

        [System.Xml.Serialization.XmlElement("Username")]
        public string Username { get; set; }

        [System.Xml.Serialization.XmlElement("Zipcode")]
        public string ZipCode { get; set; }

        [System.Xml.Serialization.XmlElement("BirthDay")]
        public string BirthDay { get; set; }

        [System.Xml.Serialization.XmlElement("Gender")]
        public string Gender { get; set; }

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
                DevKotCollectionEventArgs devent = new DevKotCollectionEventArgs();
                devent.itemcontent = value;
                devent.username = "Cartis Rene Claudia Kotoucek";
                OnAddItemEvent(devent);
            }
        }
    }
}
