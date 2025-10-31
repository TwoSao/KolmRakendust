using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Juhendid
{
    // See on mälumängu vorm. Siin saab mälu harjutada.
    public partial class Form3 : Form
    {
        // Siin on kõik sümbolid. Iga sümbolit on kaks tükki.
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "b", "b",
            "v", "v", "w", "w", "z", "z", "p", "p"
        };

        Label firstClicked = null;   // Esimene valitud ruut
        Label secondClicked = null;  // Teine valitud ruut
        Random random = new Random();

        TableLayoutPanel tableLayoutPanel1;
        Timer timer1;
        
        // Lisafunktsioonid
        int moveCount = 0;           // Käikude arv
        Label moveCountLabel;        // Näitab käikude arvu
        Button hintButton;           // Vihje nupp
        Timer gameTimer;             // Mängu taimer
        int gameTime = 0;            // Mängu aeg sekundites
        Label gameTimeLabel;         // Näitab mängu aega
        int hintsUsed = 0;           // Kasutatud vihjete arv
        Label hintsLabel;            // Näitab vihjete arvu

        // See funktsioon teeb mälumängu valmis.
        public Form3()
        {
            // === Akna tegemine ===
            this.Text = "Sobitusmäng";
            this.ClientSize = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.SteelBlue;
            
            // === Info paneeli tegemine ===
            Panel infoPanel = new Panel();
            infoPanel.Width = 150;
            infoPanel.Dock = DockStyle.Right;
            infoPanel.BackColor = Color.LightSteelBlue;
            
            // Käikude loendur
            moveCountLabel = new Label();
            moveCountLabel.Text = "Käigud: 0";
            moveCountLabel.Location = new Point(10, 20);
            moveCountLabel.Size = new Size(130, 30);
            moveCountLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            moveCountLabel.ForeColor = Color.DarkBlue;
            infoPanel.Controls.Add(moveCountLabel);
            
            // Mängu taimer
            gameTimeLabel = new Label();
            gameTimeLabel.Text = "Aeg: 0s";
            gameTimeLabel.Location = new Point(10, 60);
            gameTimeLabel.Size = new Size(130, 30);
            gameTimeLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            gameTimeLabel.ForeColor = Color.DarkBlue;
            infoPanel.Controls.Add(gameTimeLabel);
            
            // Vihjete loendur
            hintsLabel = new Label();
            hintsLabel.Text = "Vihjed: 0";
            hintsLabel.Location = new Point(10, 100);
            hintsLabel.Size = new Size(130, 30);
            hintsLabel.Font = new Font("Arial", 10, FontStyle.Bold);
            hintsLabel.ForeColor = Color.DarkBlue;
            infoPanel.Controls.Add(hintsLabel);
            
            // Vihje nupp
            hintButton = new Button();
            hintButton.Text = "💡 Vihje";
            hintButton.Location = new Point(10, 150);
            hintButton.Size = new Size(120, 40);
            hintButton.Font = new Font("Arial", 9, FontStyle.Bold);
            hintButton.BackColor = Color.LightBlue;
            hintButton.ForeColor = Color.DarkBlue;
            hintButton.Click += HintButton_Click;
            infoPanel.Controls.Add(hintButton);
            
            this.Controls.Add(infoPanel);

            // === Tabeli tegemine (4x4) ===
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.BackColor = Color.SteelBlue;
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.ColumnCount = 4;

            for (int i = 0; i < 4; i++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
                tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            }

            // === 16 ruudu lisamine ===
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Label lbl = new Label();
                    lbl.Dock = DockStyle.Fill;
                    lbl.TextAlign = ContentAlignment.MiddleCenter;
                    lbl.Font = new Font("Webdings", 36, FontStyle.Bold);
                    lbl.BackColor = Color.SteelBlue;
                    lbl.ForeColor = Color.SteelBlue;
                    lbl.Click += label_Click;
                    tableLayoutPanel1.Controls.Add(lbl, col, row);
                }
            }

            this.Controls.Add(tableLayoutPanel1);

            // === Taimeri tegemine ===
            timer1 = new Timer();
            timer1.Interval = 750;
            timer1.Tick += timer1_Tick;
            
            // === Mängu taimeri tegemine ===
            gameTimer = new Timer();
            gameTimer.Interval = 1000; // 1 секунда
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            // === Sümbolite jagamine ===
            AssignIconsToSquares();
        }

        // See funktsioon jagab sümbolid juhuslikult.
        private void AssignIconsToSquares()
        {
            List<string> iconsCopy = new List<string>(icons); // копия списка
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(iconsCopy.Count);
                    iconLabel.Text = iconsCopy[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor; // peidame sümboli
                    iconsCopy.RemoveAt(randomNumber);
                }
            }
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                return; // ootame kuni taimer lõpetab

            Label clickedLabel = sender as Label;
            if (clickedLabel == null)
                return;

            if (clickedLabel.ForeColor == Color.Black)
                return; // juba avatud

            // Esimene sümbol
            if (firstClicked == null)
            {
                firstClicked = clickedLabel;
                firstClicked.ForeColor = Color.Black;
                firstClicked.BackColor = Color.LightBlue;
                return;
            }

            // Teine sümbol
            secondClicked = clickedLabel;
            secondClicked.ForeColor = Color.Black;
            
            // Suurendame käikude arvu
            moveCount++;
            moveCountLabel.Text = $"Käigud: {moveCount}";

            if (firstClicked.Text == secondClicked.Text)
            {
                // Paar leitud!
                firstClicked.BackColor = Color.LightSteelBlue;
                secondClicked.BackColor = Color.LightSteelBlue;
                
                firstClicked = null;
                secondClicked = null;
                
                // Kontrollime võitu
                CheckForWinner();
                return;
            }

            // Если не совпало - подсвечиваем темно-синим
            secondClicked.BackColor = Color.DarkBlue;
            firstClicked.BackColor = Color.DarkBlue;
            
            // Käivitame taimeri peitmiseks
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            
            // Tagastame tavalise tausta värvi
            firstClicked.BackColor = Color.SteelBlue;
            secondClicked.BackColor = Color.SteelBlue;

            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null && iconLabel.ForeColor == iconLabel.BackColor)
                    return; // veel on peidetud ruute
            }

            gameTimer.Stop();
            
            // Tulemuse hindamine
            string performance = GetPerformanceRating();
            string message = $"🎉 Õnnitlen! Leidsid kõik paarid!\n\n" +
                           $"🕒 Aeg: {gameTime}s\n" +
                           $"👆 Käigud: {moveCount}\n" +
                           $"🏆 Hinnang: {performance}";
            
            MessageBox.Show(message, "Võit!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            MessageBox.Show("Tänan mängimast!", "Mäng lõppenud", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
        
        private string GetPerformanceRating()
        {
            // Hindamine aja ja käikude põhjal
            if (gameTime <= 30 && moveCount <= 12)
                return "⭐ Suurepärane!";
            else if (gameTime <= 60 && moveCount <= 20)
                return "👍 Väga hea!";
            else if (gameTime <= 90 && moveCount <= 30)
                return "😊 Hea!";
            else
                return "💪 Harjuta veel!";
        }
        
        // === UUED FUNKTSIOONID ===
        
        // See funktsioon loendab mängu aega.
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            gameTime++;
            gameTimeLabel.Text = $"Aeg: {gameTime}s";
        }
        
        // See funktsioon annab vihje.
        private void HintButton_Click(object sender, EventArgs e)
        {
            if (hintsUsed >= 3)
            {
                MessageBox.Show("Oled juba kasutanud kõik 3 vihjet!", "Vihjed otsas", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // Leiame juhuslikud peidetud kaardid
            List<Label> hiddenLabels = new List<Label>();
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label label = control as Label;
                if (label != null && label.ForeColor == label.BackColor)
                    hiddenLabels.Add(label);
            }
            
            if (hiddenLabels.Count < 2)
            {
                MessageBox.Show("Mäng on peaaegu lõppenud!", "Vihje", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            // Leiame paari ühesuguste sümbolitega
            Label firstHint = null, secondHint = null;
            for (int i = 0; i < hiddenLabels.Count && firstHint == null; i++)
            {
                for (int j = i + 1; j < hiddenLabels.Count; j++)
                {
                    if (hiddenLabels[i].Text == hiddenLabels[j].Text)
                    {
                        firstHint = hiddenLabels[i];
                        secondHint = hiddenLabels[j];
                        break;
                    }
                }
            }
            
            if (firstHint != null && secondHint != null)
            {
                // Näitame vihjet 2 sekundit
                firstHint.BackColor = Color.LightBlue;
                secondHint.BackColor = Color.LightBlue;
                
                Timer hintTimer = new Timer();
                hintTimer.Interval = 2000;
                hintTimer.Tick += (s, args) => {
                    firstHint.BackColor = Color.SteelBlue;
                    secondHint.BackColor = Color.SteelBlue;
                    hintTimer.Stop();
                    hintTimer.Dispose();
                };
                hintTimer.Start();
                
                hintsUsed++;
                hintsLabel.Text = $"Vihjed: {hintsUsed}";
                
                if (hintsUsed >= 3)
                    hintButton.Enabled = false;
            }
        }
    }
}
