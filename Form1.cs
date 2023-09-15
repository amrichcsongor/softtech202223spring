using System;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        System.Timers.Timer timer = new System.Timers.Timer();
        K�rtya el�z�K�rtya;
        K�rtya el�z�el�ttiK�rtya;
        public Form1()
        {
            InitializeComponent();
        }


        int kattSz�m = 0;
        int el�z�el�ttiEllen�zr� = 0;
        int joK�serlet = 0;
        int rosszK�s�rlet = 0;
        int k�rtyaMarad�k = 0;
        int invisible = 0;
        int jatekmeret;
        int mp = 0;
        DateTime startTime;

        private void K_Click(object? sender, EventArgs e)
        {
            //K�rty�ra kattintunk e
            if (sender is K�rtya)
            {   
                
                //2. kattint�sig sz�mol, ut�na 2 marad �s mindig el�z� k�rty�b�l el�z� el�tti lesz
                if (el�z�el�ttiEllen�zr� < 2)
                {
                    el�z�el�ttiEllen�zr�++;
                }

                K�rtya k = (K�rtya)sender;

                //Ha ugyanarra kattintunk kil�p
                if (k == el�z�K�rtya)
                {
                    return;
                }

                //Kattint�sok sz�mol�sa
                kattSz�m++;

                //Ha nem az els� l�p�sn�l vagyunk
                if (el�z�K�rtya != null)
                {

                    //Ha p�rt tal�lunk �s nem a 3. kattint�sn�l vagyunk
                    if (k.k�pSz�m == el�z�K�rtya.k�pSz�m && k != el�z�K�rtya && kattSz�m != 3)
                    {

                        k.Visible = false;
                        el�z�K�rtya.Visible = false;
                        joK�serlet++;
                    }

                    //Ha nem p�rt tal�lunk
                    if (k.k�pSz�m != el�z�K�rtya.k�pSz�m && k != el�z�K�rtya)
                    {
                        rosszK�s�rlet++;
                    }

                    //3. kattint�sra visszaford�t �s 1-re �ll�tja a kattint�sok sz�m�t
                    if (kattSz�m == 3)
                    {
                        el�z�el�ttiK�rtya.Image = Bitmap.FromFile(Properties.Settings.Default.k�pK�nyvt�r + "card_back" + Properties.Settings.Default.k�pUt�lag);
                        el�z�K�rtya.Image = Bitmap.FromFile(Properties.Settings.Default.k�pK�nyvt�r + "card_back" + Properties.Settings.Default.k�pUt�lag);
                        kattSz�m = 1;
                    }
                }

                //2. kattint�sra el�j�n az el�z�el�ttiK�rtya
                if (el�z�el�ttiEllen�zr� == 2)
                {
                    el�z�el�ttiK�rtya = el�z�K�rtya;
                }
                el�z�K�rtya = k;

                //V�ge vizsg�lat
                foreach (Control item in Controls)
                {
                    if (item is K�rtya && item.Visible == false)
                    {
                        invisible++;
                    }

                    //Ha annyi l�thatatlan lesz mint amekkora a p�lya akkor v�ge
                    if (invisible == jatekmeret)
                    {
                        timer.Stop();
                        timer1.Stop();
                        TimeSpan elapsedTime = DateTime.Now - startTime;

                        MessageBox.Show("V�ge a j�t�knak");
                        MessageBox.Show("J� �s rossz v�laszt�sok ar�nya: " + joK�serlet + ":" + rosszK�s�rlet / 2);
                        MessageBox.Show("Eltelt id�: " + elapsedTime.ToString(@"hh\:mm\:ss"));
                        
                    }
                }
                invisible = 0;
            }

        }

        int[] Kever�s(int k�rtyasz�m)
        {
            int[] t�mb = new int[k�rtyasz�m];
            Random rnd = new Random();
            int[] randomt�mb = Enumerable.Range(1, 50).ToArray();
                for (int i = randomt�mb.Length - 1; i > 0; i--)
                {
                    int j = rnd.Next(i + 1);
                    int temp = randomt�mb[i];
                    randomt�mb[i] = randomt�mb[j];
                    randomt�mb[j] = temp;
                }



            for (int i = 0; i < k�rtyasz�m / 2; i++)
            {
                t�mb[i] = randomt�mb[i + 1];
                t�mb[i + k�rtyasz�m / 2] = randomt�mb[i + 1];
            }


            for (int i = 0; i < k�rtyasz�m; i++)
            {
                int egyik = rnd.Next(k�rtyasz�m);
                int m�sik = rnd.Next(k�rtyasz�m);

                int k�ztes = t�mb[egyik];
                t�mb[egyik] = t�mb[m�sik];
                t�mb[m�sik] = k�ztes;

            }

            return t�mb;
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
            BackgroundImage = Bitmap.FromFile(Properties.Settings.Default.h�tt�rk�p);
            Width = BackgroundImage.Width + 20;
            Height = BackgroundImage.Height;

            int sorSz�m = 0;
            int[] t = Kever�s(jatekmeret);

            for (int s = 0; s < 2; s++)
            {
                for (int o = 0; o < 2; o++)
                {
                    K�rtya k = new K�rtya(s, o, t[sorSz�m]);
                    Controls.Add(k);
                    k.Click += K_Click;
                    sorSz�m++;
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
            BackgroundImage = Bitmap.FromFile(Properties.Settings.Default.h�tt�rk�p);
            Width = BackgroundImage.Width + 70;
            Height = BackgroundImage.Height;

            int sorSz�m = 0;
            int[] t = Kever�s(jatekmeret);

            for (int s = 0; s < 4; s++)
            {
                for (int o = 0; o < 4; o++)
                {
                    K�rtya k = new K�rtya(s, o, t[sorSz�m]);
                    Controls.Add(k);
                    k.Click += K_Click;
                    sorSz�m++;
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
            BackgroundImage = Bitmap.FromFile(Properties.Settings.Default.h�tt�rk�p);
            Width = BackgroundImage.Width + 200;
            Height = BackgroundImage.Height;

            int sorSz�m = 0;
            int[] t = Kever�s(jatekmeret);

            for (int s = 0; s < 6; s++)
            {
                for (int o = 0; o < 6; o++)
                {
                    K�rtya k = new K�rtya(s, o, t[sorSz�m]);
                    Controls.Add(k);
                    k.Click += K_Click;
                    sorSz�m++;
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