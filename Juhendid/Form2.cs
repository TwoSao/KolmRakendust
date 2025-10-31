using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace Juhendid
{
    // See on matemaatika testi vorm. Siin saab arvutada.
    public partial class Form2 : Form
    {
        Panel panel1 = new Panel();
        Label timeLbl = new Label();
        Label countdownLbl = new Label();
        Button checkBtn = new Button();

        Label mark = new Label();
        Label mark2 = new Label();
        Label mark3 = new Label();

        Label q1_1 = new Label();
        Label q1_2 = new Label();
        Label q2_1 = new Label();
        Label q2_2 = new Label();
        Label q3_1 = new Label();
        Label q3_2 = new Label();

        TextBox ans1 = new TextBox();
        TextBox ans2 = new TextBox();
        TextBox ans3 = new TextBox();

        Button startBtn = new Button();
        Button resetBtn = new Button();
        Button pauseBtn = new Button();

        Random rnd = new Random();

        int i1_1, i1_2, i2_1, i2_2, i3_1, i3_2;
        int timeLeft = 30;
        Timer quizTimer = new Timer();

        // See funktsioon teeb matemaatika testi valmis.
        public Form2()
        {
            InitializeComponent();
            this.Text = "Matemaatikaviktoriin";
            this.Width = 700;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;

            // === Paneel ===
            panel1.Dock = DockStyle.Fill;
            panel1.BackColor = Color.Bisque;
            Controls.Add(panel1);

            // === Aja näitaja ===
            timeLbl.Text = "Aega jäänud:";
            timeLbl.Location = new Point(450, 10);
            timeLbl.AutoSize = true;
            timeLbl.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            panel1.Controls.Add(timeLbl);

            countdownLbl.Text = "30";
            countdownLbl.Location = new Point(580, 10);
            countdownLbl.AutoSize = true;
            countdownLbl.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            panel1.Controls.Add(countdownLbl);

            // === Alustamise nupp ===
            startBtn.Text = "Alusta viktoriini";
            startBtn.Location = new Point(280, 400);
            startBtn.Size = new Size(120, 40);
            startBtn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            startBtn.BackColor = Color.LightGreen;
            startBtn.Click += StartQuiz;
            panel1.Controls.Add(startBtn);

            // === Lähtestamise nupp ===
            resetBtn.Text = "Lähtesta";
            resetBtn.Size = new Size(120, 40);
            resetBtn.Location = new Point(420, 400);
            resetBtn.BackColor = Color.LightCoral;
            resetBtn.Click += ResetBtn_Click;
            panel1.Controls.Add(resetBtn);

            // === Pausi nupp ===
            pauseBtn.Text = "Paus";
            pauseBtn.Size = new Size(120, 40);
            pauseBtn.Location = new Point(140, 400);
            pauseBtn.BackColor = Color.LightGray;
            pauseBtn.Click += PauseBtn_Click;
            panel1.Controls.Add(pauseBtn);

            // === Ülesannete tegemine ===
            CreateQuestionRow(q1_1, q1_2, mark, ans1, "+", 80);
            CreateQuestionRow(q2_1, q2_2, mark2, ans2, "-", 140);
            CreateQuestionRow(q3_1, q3_2, mark3, ans3, "×", 200);

            // === Vastuste kontrollimise nupp ===
            checkBtn.Text = "Kontrolli vastuseid";
            checkBtn.Location = new Point(280, 320);
            checkBtn.Size = new Size(150, 40);
            checkBtn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            checkBtn.BackColor = Color.LightBlue;
            checkBtn.Click += CheckAnswersClick;
            panel1.Controls.Add(checkBtn);

            // === Taimer ===
            quizTimer.Interval = 1000;
            quizTimer.Tick += QuizTimer_Tick;
        }

        // See funktsioon teeb ühe ülesande rea.
        private void CreateQuestionRow(Label left, Label right, Label op, TextBox ans, string symbol, int y)
        {
            left.Text = "?";
            left.Location = new Point(150, y);
            left.Size = new Size(60, 40);
            left.Font = new Font("Segoe UI", 20);
            left.TextAlign = ContentAlignment.MiddleCenter;

            op.Text = symbol;
            op.Location = new Point(230, y);
            op.Size = new Size(40, 40);
            op.Font = new Font("Segoe UI", 20);
            op.TextAlign = ContentAlignment.MiddleCenter;

            right.Text = "?";
            right.Location = new Point(290, y);
            right.Size = new Size(60, 40);
            right.Font = new Font("Segoe UI", 20);
            right.TextAlign = ContentAlignment.MiddleCenter;

            ans.Location = new Point(380, y + 5);
            ans.Size = new Size(100, 40);
            ans.Font = new Font("Segoe UI", 18);

            panel1.Controls.AddRange(new Control[] { left, op, right, ans });
        }

        // See funktsioon alustab uut testi.
        private void StartQuiz(object? sender, EventArgs e)
        {
            i1_1 = rnd.Next(1, 50);
            i1_2 = rnd.Next(1, 50);
            i2_1 = rnd.Next(1, 100);
            i2_2 = rnd.Next(1, 100);
            i3_1 = rnd.Next(1, 10);
            i3_2 = rnd.Next(1, 10);

            q1_1.Text = i1_1.ToString();
            q1_2.Text = i1_2.ToString();
            q2_1.Text = i2_1.ToString();
            q2_2.Text = i2_2.ToString();
            q3_1.Text = i3_1.ToString();
            q3_2.Text = i3_2.ToString();

            ans1.Text = ans2.Text = ans3.Text = "";

            timeLeft = 30;
            countdownLbl.Text = "30";
            quizTimer.Start();
        }

        // See funktsioon loendab aega tagurpidi.
        private void QuizTimer_Tick(object? sender, EventArgs e)
        {
            timeLeft--;
            countdownLbl.Text = timeLeft.ToString();

            if (timeLeft <= 0)
            {
                quizTimer.Stop();
                CheckAnswers();
            }
        }

        private void CheckAnswersClick(object? sender, EventArgs e)
        {
            quizTimer.Stop();
            CheckAnswers();
        }

        // See funktsioon kontrollib vastuseid.
        private void CheckAnswers()
        {
            int correct = 0;

            if (int.TryParse(ans1.Text, out int a1) && a1 == i1_1 + i1_2) correct++;
            if (int.TryParse(ans2.Text, out int a2) && a2 == i2_1 - i2_2) correct++;
            if (int.TryParse(ans3.Text, out int a3) && a3 == i3_1 * i3_2) correct++;

            MessageBox.Show($"Said {correct} õiget vastust 3-st!", "Tulemused");
        }

        // See funktsioon kustutab kõik vastused.
        private void ResetBtn_Click(object? sender, EventArgs e)
        {
            ans1.Text = ans2.Text = ans3.Text = "";
        }

        // See funktsioon peatab või jätkab testi.
        private void PauseBtn_Click(object? sender, EventArgs e)
        {
            if (quizTimer.Enabled)
            {
                quizTimer.Stop();
                pauseBtn.Text = "Jätka";
            }
            else
            {
                quizTimer.Start();
                pauseBtn.Text = "Paus";
            }
        }
    }
}
