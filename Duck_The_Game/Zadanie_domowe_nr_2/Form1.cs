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
using WMPLib;// kwestia muzyki

namespace Zadanie_domowe_nr_2
{
    public partial class Form1 : Form
    {
        int hits; //licznik trafien
        int counter;//licznik ile lacznie kaczek
        bool Duck_is;

        Image background2 = Image.FromFile("strzelnica.jpg");

        WMPLib.WindowsMediaPlayer player = new WMPLib.WindowsMediaPlayer();// kwestia muzyki

        public Form1()
        {
            InitializeComponent();

            player.URL = "DuckMusic.wav";// kwestia muzyki
            player.controls.stop();// kwestia muzyki
        }

        private void duck() //funkcja na kaczuchce
        {
            Duck_is = true;
            timer1.Enabled = true; //to wystarczy bez timer1 stop i start a domysnie timer1.Enabled = false; w panelu po prawej na dole

            Button kaczka = new Button(); //przycisk o nazwie kaczka!
            kaczka.Size = new System.Drawing.Size(60, 89);
            //kaczka.Text = "Try to hit me!"; //napis na przycisku
            kaczka.Name = "kaczucha"; //trzeba nadac nazwe kaczce aby znalazlo
            kaczka.Image = Image.FromFile("kill the duck.png");


            int czas = 3000 / trackBar1.Value; //uzaleznione od suwaka wartosc 4000 milisekund od trackBar1.Val czyli wpierw 4000/1 potem 4000/2 i tak do 5
            double czas_min = czas * 0.8; //roznica 20% podstawowego czasu
            double czas_maks = czas * 1.2; //roznica 20% podstawowego czasu

            Random time = new Random();
            timer1.Interval = time.Next((int)czas_min, (int)czas_maks); //interwal z roznic 20%

            Random oX = new Random();
            Random oY = new Random();

            int X = (76 * timer1.Interval) % 700; //Losowanie patternu jak się da
            int Y = (76 * timer1.Interval) % 300; //Losowanie patternu jak się da
            kaczka.Location = new System.Drawing.Point(oX.Next(0,X), oY.Next(0,Y)); //startowa lokacja losowa

            kaczka.Click += new System.EventHandler(this.bij); //bij == klikniecie w kaczke
            Controls.Add(kaczka);
        }

        private void bij(object sender, EventArgs e)
        {
            Duck_is = false;
            hits++;

            int time = 2000 / trackBar1.Value;
            timer1.Interval = time;

            var pom = Controls.Find("kaczucha",true); //czy jest button kaczka, gdy istnieje
            if (pom.Count() != 0)
            {
                pom[0].Dispose(); //usuniecie
            }

        }
        private void bij()
        {
            Duck_is = false;

            int time = 2000 / trackBar1.Value;
            timer1.Interval = time;

            var pom = Controls.Find("kaczucha", true); //czy jest button kaczka, gdy istnieje
            if(pom.Count() !=0)
            {
                pom[0].Dispose(); //usuniecie
            }
        }

        private void button1_Click(object sender, EventArgs e) //Button: Start the game!
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Enter nickname before started: ");
            }
            else
            {
                button1.Visible = false;
                textBox1.Visible = false;
                label1.Visible = false;

                player.controls.play();

                button2.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                textBox2.Visible = true;
                trackBar1.Visible = true;
                timer1.Start();
                timer1.Enabled = true;
                
                this.BackgroundImage = background2;
                textBox1.Text = "";
                duck();
            }
        }

        private void timer1_Tick(object sender, EventArgs e) //nasz ten timer
        {
            if(Duck_is)
            {
                bij();
                duck();
            }
            else
            {
                duck();
            }
            wynik();
        }

        private void save()
        {
            using (StreamWriter sw = new StreamWriter("Wyniki.txt", true))
            {
                DateTime date1 = DateTime.Now;
                string word = "Nazwa gracza: " + textBox1.Text + "\n" + "Wynik: " + Convert.ToString(hits) + " / " + Convert.ToString(counter) + "\n" +  "Godzina: " + date1 + "\n";
                sw.WriteLine(word);
            }
        }

        private void wynik()
        {
            counter++;
            textBox2.Text = hits.ToString() + "/" + counter.ToString();
        }

        private void button2_Click(object sender, EventArgs e) //Button - Stop the game!
        {
            save();

            button1.Visible = true;
            textBox1.Visible = true;
            label1.Visible = true;

            button2.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            textBox2.Visible = false;
            trackBar1.Visible = false;

            timer1.Stop();
            timer1.Enabled = false;
            player.controls.stop();
            this.BackgroundImage = Image.FromFile("tlo.jpg");
            hits = 0;
            counter = 0;
            textBox2.Text = "";
            bij();
        }

        //przez przypadek wlaczylem f-cje ponizej, nic ma sie tu nie dziac bo jest niepotrzebna
        private void trackBar1_Scroll(object sender, EventArgs e)
        {

        }
    }
}
