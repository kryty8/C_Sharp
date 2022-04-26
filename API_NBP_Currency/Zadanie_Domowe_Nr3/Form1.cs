using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Globalization;

namespace Zadanie_Domowe_Nr3
{
    public partial class Form1 : Form
    {
        internal static Form2 form2; //potrzebne aby z forma 2 do 1
        internal static Form1 form1; //potrzebne aby z forma 2 do 1

        public static List<string> lista = new List<string>(); //statyczna lista bo nie wiadomo ile elementow w srodku bedzie

        public Form1()
        {
            new JObject();
            InitializeComponent();
            form1 = this; //potrzebne aby z forma 2 do 1
            timer1.Interval = 30000; //co 30 sekund pobiera w trakcie działania progamu. W milisekundach
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            form2 = new Form2(this); //potrzebne aby z forma 2 do 1
            form2.Show(); //potrzebne aby z forma 2 do 1
        }

        private void add_lista(string val, string data, string kurs)
        {
            lista.Add(val);

            using (StreamWriter save = new StreamWriter("Kursy/" + val + ".txt", true)) //dodaje do pliku wartosci
            {
                string linia = val + " | " + data + " | " + kurs + " | ";
                save.WriteLine(linia);
            }
        }

        public void dodaj_waluta(string waluta)
        {
            try
            {
                string url = "http://api.nbp.pl/api/exchangerates/rates/a/" + waluta + "/?format=json";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                var status = response.StatusCode;
                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
                dynamic data = JObject.Parse(content);  //var - kompilacja dynamic - runtime

                bool flaga = false; //flaga aby takie same stringi chociaz nie wchodzily; usd i drugi raz usd sie nie wpisze -> jest juz ta waluta

                foreach(string element in lista)
                {
                    if(element == waluta)
                    {
                        flaga = true;
                        MessageBox.Show("Podana waluta znajduje sie na liscie");
                    }
                }
                if(flaga == false)
                {
                    add_lista(waluta, (string)data.rates[0].effectiveDate, (string)data.rates[0].mid); //dodawanie do listy walut jesli nie ma duplikatu
                } 
            }
            catch(Exception blad)
            {
                MessageBox.Show(blad.Message);
            }
        }

        public void pokaz_liste()
        {
            textBox2.Text = "";
            foreach(string element in lista)
            {
                textBox2.Text += element + ", ";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < lista.Count(); i++)
            {
                string url = "http://api.nbp.pl/api/exchangerates/rates/a/" + lista[i] + "/?format=json";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                var status = response.StatusCode;
                string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
                dynamic data = JObject.Parse(content);  //var - kompilacja dynamic - runtime

                string name = lista[i] + ".txt";
                string val = lista[i];
                string date = (string)data.rates[0].effectiveDate;
                string kurs = (string)data.rates[0].mid;

                using (StreamWriter save = new StreamWriter("Kursy/" + name, true))
                {
                    string linia = val + " | " + date + " | " + kurs + " | ";
                    save.WriteLine(linia);
                }
            }
        }

        private double odczyt(string sciezka)
        {
            double suma = 0;
            int razem = 0;
            try
            {
                using (StreamReader sr = new StreamReader("Kursy/" + sciezka + ".txt"))
                {
                    string linia;
                    while ((linia = sr.ReadLine()) != null)
                    {
                        string[] r = linia.Split('|');
                        double wynik = double.Parse(r[2], CultureInfo.InvariantCulture);
                        suma += wynik;
                        razem++;
                    }
                }
                return (suma / razem);
            }
            catch
            {
                Console.WriteLine("I can't read the file");
                return -1; //musi cos zwracac, wtedy bedzie blad i w textbox pokaze -1 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string d = textBox1.Text;
                textBox3.Text = odczyt(d).ToString();
            }
            catch
            {
                MessageBox.Show("Błędna waluta!");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
