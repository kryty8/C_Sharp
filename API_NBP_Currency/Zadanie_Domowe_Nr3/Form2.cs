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
    public partial class Form2 : Form
    {
        Form1 form1; //potrzebne aby z forma 2 do 1s

        public Form2(Form1 f1)
        {
            InitializeComponent();
            form1 = f1; //potrzebne aby z forma 2 do 1
        }

        private void Submit_Click(object sender, EventArgs e)
        {
            form1.dodaj_waluta(textBox1.Text.ToUpper()); //robione wszystko w form1 a uzywane w form2 .ToUpper aby na wielkie litery dawało stringa przeciazenie 
            form1.pokaz_liste(); //
            textBox1.Text = "";
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            //przez przypadek
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
