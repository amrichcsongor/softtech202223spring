using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        Kártya elõzõKártya;
        Kártya elõzõelöttiKártya;
        public Form1()
        {
            InitializeComponent();
        }


        int kattSzám = 0;
        int elõzõelöttiEllenõzrõ = 0;
        int joKíserlet = 0;
        int rosszKísérlet = 0;
        int kártyaMaradék = 0;
        int invisible = 0;
        int jatekmeret;
        int mp = 0;
        DateTime startTime;

        private void K_Click(object? sender, EventArgs e)
        {
            //Kártyára kattintunk e
            if (sender is Kártya)
            {   
                
                //2. kattintásig számol, utána 2 marad és mindig elõzõ kártyából elõzõ elötti lesz
                if (elõzõelöttiEllenõzrõ < 2)
                {
                    elõzõelöttiEllenõzrõ++;
                }

                Kártya k = (Kártya)sender;

                //Ha ugyanarra kattintunk kilép
                if (k == elõzõKártya)
                {
                    return;
                }

                //Kattintások számolása
                kattSzám++;

                //Ha nem az elsõ lépésnél vagyunk
                if (elõzõKártya != null)
                {

                    //Ha párt találunk és nem a 3. kattintásnál vagyunk
                    if (k.képSzám == elõzõKártya.képSzám && k != elõzõKártya && kattSzám != 3)
                    {

                        k.Visible = false;
                        elõzõKártya.Visible = false;
                        joKíserlet++;
                    }

                    //Ha nem párt találunk
                    if (k.képSzám != elõzõKártya.képSzám && k != elõzõKártya)
                    {
                        rosszKísérlet++;
                    }

                    //3. kattintásra visszafordít és 1-re állítja a kattintások számát
                    if (kattSzám == 3)
                    {
                        elõzõelöttiKártya.Image = Bitmap.FromFile(Properties.Settings.Default.képKönyvtár + "card_back" + Properties.Settings.Default.képUtólag);
                        elõzõKártya.Image = Bitmap.FromFile(Properties.Settings.Default.képKönyvtár + "card_back" + Properties.Settings.Default.képUtólag);
                        kattSzám = 1;
                    }
                }

                //2. kattintásra elõjön az elõzõelöttiKártya
                if (elõzõelöttiEllenõzrõ == 2)
                {
                    elõzõelöttiKártya = elõzõKártya;
                }
                elõzõKártya = k;

                //Vége vizsgálat
                foreach (Control item in Controls)
                {
                    if (item is Kártya && item.Visible == false)
                    {
                        invisible++;
                    }

                    //Ha annyi láthatatlan lesz mint amekkora a pálya akkor vége
                    if (invisible == jatekmeret)
                    {
                        timer.Stop();
                        timer1.Stop();
                        TimeSpan elapsedTime = DateTime.Now - startTime;

                        MessageBox.Show("Vége a játéknak");
                        MessageBox.Show("Jó és rossz választások aránya: " + joKíserlet + ":" + rosszKísérlet / 2);
                        MessageBox.Show("Eltelt idõ: " + elapsedTime.ToString(@"hh\:mm\:ss"));
                        
                    }
                }
                invisible = 0;
            }

        }

        int[] Keverés(int kártyaszám)
        {
            int[] tömb = new int[kártyaszám];
            Random rnd = new Random();
            int[] randomtömb = Enumerable.Range(1, 50).ToArray();
                for (int i = randomtömb.Length - 1; i > 0; i--)
                {
                    int j = rnd.Next(i + 1);
                    int temp = randomtömb[i];
                    randomtömb[i] = randomtömb[j];
                    randomtömb[j] = temp;
                }



            for (int i = 0; i < kártyaszám / 2; i++)
            {
                tömb[i] = randomtömb[i + 1];
                tömb[i + kártyaszám / 2] = randomtömb[i + 1];
            }


            for (int i = 0; i < kártyaszám; i++)
            {
                int egyik = rnd.Next(kártyaszám);
                int másik = rnd.Next(kártyaszám);

                int köztes = tömb[egyik];
                tömb[egyik] = tömb[másik];
                tömb[másik] = köztes;

            }

            return tömb;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            jatekmeret = 4;
            timer.Start();
            timer1.Start();
            startTime = DateTime.Now;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            BackgroundImage = Bitmap.FromFile(Properties.Settings.Default.háttérkép);
            Width = BackgroundImage.Width + 20;
            Height = BackgroundImage.Height;

            int sorSzám = 0;
            int[] t = Keverés(jatekmeret);

            for (int s = 0; s < 2; s++)
            {
                for (int o = 0; o < 2; o++)
                {
                    Kártya k = new Kártya(s, o, t[sorSzám]);
                    Controls.Add(k);
                    k.Click += K_Click;
                    sorSzám++;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            jatekmeret = 16;
            timer.Start();
            startTime = DateTime.Now;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            BackgroundImage = Bitmap.FromFile(Properties.Settings.Default.háttérkép);
            Width = BackgroundImage.Width + 70;
            Height = BackgroundImage.Height;

            int sorSzám = 0;
            int[] t = Keverés(jatekmeret);

            for (int s = 0; s < 4; s++)
            {
                for (int o = 0; o < 4; o++)
                {
                    Kártya k = new Kártya(s, o, t[sorSzám]);
                    Controls.Add(k);
                    k.Click += K_Click;
                    sorSzám++;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            jatekmeret = 36;
            timer.Start();
            startTime = DateTime.Now;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            BackgroundImage = Bitmap.FromFile(Properties.Settings.Default.háttérkép);
            Width = BackgroundImage.Width + 200;
            Height = BackgroundImage.Height;

            int sorSzám = 0;
            int[] t = Keverés(jatekmeret);

            for (int s = 0; s < 6; s++)
            {
                for (int o = 0; o < 6; o++)
                {
                    Kártya k = new Kártya(s, o, t[sorSzám]);
                    Controls.Add(k);
                    k.Click += K_Click;
                    sorSzám++;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            mp++;
            label1.Text = "Time: " + mp.ToString();
        }
    }
}