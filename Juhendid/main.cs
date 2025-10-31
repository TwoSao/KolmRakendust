using System;
using System.Drawing;
using System.Windows.Forms;

namespace Juhendid
{
    // See on peamine vorm. See näitab menüüd.
    public partial class MainForm : Form
    {
        // See funktsioon teeb akna valmis.
        public MainForm()
        {
            // Akna nimi on "Peamenüü".
            this.Text = "Peamenüü";
            // Akna suurus on 350x280 pikslit.
            this.Size = new Size(350, 280);
            // Aken avaneb ekraani keskel.
            this.StartPosition = FormStartPosition.CenterScreen;

            // Esimene nupp. See avab pildivaataja.
            Button btn1 = new Button();
            btn1.Text = "picture viewer";
            btn1.Size = new Size(150, 40);
            btn1.Location = new Point(100, 50);
            // Kui vajutada nuppu, avaneb Form1.
            btn1.Click += (s, e) => new Form1().Show();

            // Teine nupp. See avab matemaatika testi.
            Button btn2 = new Button();
            btn2.Text = "math quiz";
            btn2.Size = new Size(150, 40);
            btn2.Location = new Point(100, 110);
            // Kui vajutada nuppu, avaneb Form2.
            btn2.Click += (s, e) => new Form2().Show();

            // Kolmas nupp. See avab mälumängu.
            Button btn3 = new Button();
            btn3.Text = "matching game";
            btn3.Size = new Size(150, 40);
            btn3.Location = new Point(100, 170);
            // Kui vajutada nuppu, avaneb Form3.
            btn3.Click += (s, e) => new Form3().Show();

            // Kõik nupud lisatakse aknasse.
            this.Controls.Add(btn1);
            this.Controls.Add(btn2);
            this.Controls.Add(btn3);
        }
        
    }
}