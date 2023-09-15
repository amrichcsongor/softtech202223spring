using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace WinFormsApp1
{
    public class Kártya : PictureBox
    {
        public int képSzám;

        public Kártya(int sor, int oszlop, int képSzám)
        {
            this.képSzám = képSzám;

            this.Height = Properties.Settings.Default.képMéret;
            this.Width = Properties.Settings.Default.képMéret;

            this.Left = oszlop * Properties.Settings.Default.képTávolság;
            this.Top = sor * Properties.Settings.Default.képTávolság + 50;

            Lefordít();
            //Felfordít();


            this.Click += Kártya_Click;
            
        }


        private void Kártya_Click(object? sender, EventArgs e)
        {
            Felfordít();

        }

        void Felfordít()
        {
            this.Image = Bitmap.FromFile(Properties.Settings.Default.képKönyvtár + képSzám.ToString() + Properties.Settings.Default.képUtólag);
        }

        void Lefordít()
        {
            this.Image = Bitmap.FromFile(Properties.Settings.Default.képKönyvtár + "card_back" + Properties.Settings.Default.képUtólag);
        }
    }
}
