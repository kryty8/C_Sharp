using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zadanie_Domowe_Nr_1
{
    public partial class Form1 : Form
    {
        public List<int> lista = new List<int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) //generate click
        {
            lista.Clear();
            textBox2.Text = "";

            Random losowa = new Random();

            int liczby; // deklaracja dodania liczb w ramach losowania

            int ilosc; //ile bedzie liczb w length

            ilosc = Convert.ToInt32(textBox1.Text); //konwercja ze stringa na inta 

            for (int i = 0; i < ilosc; i++)
            {
                liczby = losowa.Next(0, 100);
                lista.Add(liczby);
            }

            foreach (int element in lista)
            {
                textBox2.Text += element.ToString() + " ";
            }
        }

        private void button3_Click(object sender, EventArgs e) //Submit
        {
            lista.Clear(); //By wyzerowac liste jesli sie podaje nowe inne wartosci recznie

            if(textBox2.Text != "") //sprawdzenie czy cos jest wpisane
            {
                string input = textBox2.Text;
                var numReg = new Regex(@"-*\d+"); //deklaracja aby moc wykryc ujemne liczby
                var liczczby = numReg.Matches(input).Cast<Match>().Select(m => m.Value).ToArray(); //Wykrywanie dodatnich oraz ujemnych liczb

                foreach (string element in liczczby)
                {
                    if(!string.IsNullOrEmpty(element))
                    {
                        int p = int.Parse(element);
                        lista.Add(p);
                    }
                }
                //srednia
                double srednia = System.Convert.ToDouble(lista.Average());
                textBox3.Text = srednia.ToString();
                //mediana
                lista.Sort();
                double mediana;
                if(lista.Count()%2 == 0)
                {
                    mediana = (double)(lista[(lista.Count() - 1) / 2] + lista[lista.Count() / 2]) / 2;
                }
                else
                {
                    mediana = (double)lista[lista.Count() / 2];
                }
                textBox4.Text = mediana.ToString();
                //Minimum
                double minimum = lista.Min();
                textBox5.Text = minimum.ToString();
                //Maksimum
                double maksimum = lista.Max();
                textBox6.Text = maksimum.ToString();
            }
            else
            {
                MessageBox.Show("The list is empty!");
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) //asc rosnaco
        {
            textBox8.Text = "";
            lista.Sort();
            foreach(double element in lista)
            {
                textBox8.Text += element.ToString() + " ";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e) //desc malejaco 
        {
            textBox8.Text = "";
            lista.Sort();
            lista.Reverse();
            foreach (double element in lista)
            {
                textBox8.Text += element.ToString() + " ";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            var zmienna = lista.OrderBy(x => Guid.NewGuid()).ToList(); //mieszanie liczb miedzy soba
            foreach(int element in zmienna)
            {
                textBox7.Text += element.ToString() + " ";
            }
        }
    }
}
