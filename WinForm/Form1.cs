using DSharp.FrameWork.ContactGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Data.SQLite;

namespace WinForm
{
    public partial class Form1 : Form
    {
        Counter c;
        public SQLiteConnection sqlite;
        public Form1()
        {
            InitializeComponent();
            this.c = new Counter(3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.c.ThresholdReached += C_ThresholdReached;
            this.c.Add(1);

        }

        private void C_ThresholdReached(object sender, ThresholdReachedEventArgs e)
        {
            textBox1.Text = String.Format("The threshold of {0} was reached at {1}.", e.Threshold, e.TimeReached);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DevKotCollection<String> devcol = new DevKotCollection<String>();
            DevKotCollection<Person> p = new DevKotCollection<Person>();
            devcol.AddItemEvent += Devcol_AddItemEvent;
            devcol.Add("Claudia");
        }

        private void Devcol_AddItemEvent(object sender, DevKotCollectionEventArgs e)
        {
            textBox1.Text = e.itemcontent.ToString();
            textBox1.Text += " " + e.username;

            textBox1.Text = DSharp.FrameWork.ContactGenerator.SimpleGenerator.PhoneNumber(DSharp.FrameWork.ContactGenerator.Language.US);
            textBox1.Text = DSharp.FrameWork.ContactGenerator.SimpleGenerator.AccountNumber(DSharp.FrameWork.ContactGenerator.Language.US);
            DSharp.FrameWork.ContactGenerator.Generator gen = new DSharp.FrameWork.ContactGenerator.Generator();
            gen.Settings.Language = DSharp.FrameWork.ContactGenerator.Language.US;
            gen.RecordDefinition.AddStreet();
            gen.RecordDefinition.AddGender();
            gen.RecordDefinition.AddFirstName();
            gen.RecordDefinition.AddLastName();
            gen.RecordDefinition.AddPhoneNumber();
            gen.RecordDefinition.AddEmail();
            gen.RecordDefinition.AddUsername();
            gen.RecordDefinition.AddZipcode();
            gen.RecordDefinition.AddBirthDay();




            gen.ExportOptions.Type = DSharp.FrameWork.ContactGenerator.ExportType.XML;
            gen.Settings.Language = Language.US;
            gen.ExportOptions.ListName = "Personen";

            gen.ExportOptions.ElementName = "Person";

            // gen.Generate(50);
            //gen.Export("Personen.xml");


            Personen cars = null;
            Person.ItemChanged += Person_ChangeItemEvent;
       
            string path = "Personen.xml";

            XmlSerializer serializer = new XmlSerializer(typeof(Personen));

            StreamReader reader = new StreamReader(path);
            cars = (Personen)serializer.Deserialize(reader);
            reader.Close();

            BindingSource source = new BindingSource();
         
            source.DataSource = cars.Person.ToList<Person>();
       


            listBox1.DisplayMember = "FirstName";
            listBox1.DataSource = source;
           // textBox2.DataBindings.Add(new Binding("Text", cars.Person, "FirstName"));
            textBox2.DataBindings.Add("Text", source, "FirstName", true, DataSourceUpdateMode.OnPropertyChanged);

            foreach (Person p in cars.Person)
            {

                //listBox1.Items.Add(p);

            }
            cars.Person.AddItemEvent += Person_AddItemEvent;
            cars.Person.Add(new Person() { FirstName = "Claudia Cartis", Adress = "Attemsgasse Theodor Krammerstrasse", ZipCode = "1220" });
            cars.Person.ChangeItemEvent += Person_ChangeItemEvent;
            cars.Person[cars.Person.Count - 1].FirstName = "dgfgfgfg";
          


        }

        private void Person_ChangeItemEvent(object sender, DevKotCollectionEventArgs e)
        {
            textBox1.Text = e.itemcontent.ToString() + " " + e.username;
        }

        private void Person_AddItemEvent(object sender, DevKotCollectionEventArgs e)
        {
            if(e.itemcontent.GetType()==typeof(Person))
            textBox1.Text = ((Person)e.itemcontent).Adress + " " + ((Person)e.itemcontent).ZipCode.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (!File.Exists(Application.StartupPath + "\\Experts.sqlite"))
               {

                System.Data.SQLite.SQLiteConnection.CreateFile(Application.StartupPath+ "\\Experts.sqlite");
                using (sqlite = new SQLiteConnection("Data Source=" + Application.StartupPath + "\\Experts.sqlite"))
                {
                    sqlite.Open();

                    string sql = "create table Benutzer (Id INTEGER PRIMARY KEY AUTOINCREMENT, FirstName varchar(255), LastName varchar(255), Adress varchar(255), Phone varchar(255), Email varchar(255), Username varchar(255), BirthDay varchar(255), Zipcode varchar(255), Gender varchar(255))";
                    SQLiteCommand command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();
                  


                }
            
            }
         
            //var g = moo.Person.FirstOrDefaultPersonenEntities1.Person>());
            

            //using (var mo = new PersonenEntities1())
            //{
            //    var g = new PersonenEntities1().Person;
            //}

        }
    }
}
