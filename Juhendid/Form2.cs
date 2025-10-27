using System;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer; // Используем Forms Timer, он лучше для UI

namespace Juhendid
{
    public partial class Form2 : Form
    {
        // Панель и элементы
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

        Random rnd = new Random();

        int i1_1, i1_2, i2_1, i2_2, i3_1, i3_2;
        int timeLeft = 30;
        Timer quizTimer = new Timer();

        public Form2()
        {
            InitializeComponent();
            this.Text = "Math Quiz";
            this.Width = 900;
            this.Height = 700;
            this.StartPosition = FormStartPosition.CenterScreen;

            // === Панель ===
            panel1.Dock = DockStyle.Fill;
            panel1.BackColor = Color.Bisque;
            Controls.Add(panel1);

            // === Заголовок времени ===
            timeLbl.Text = "Time left:";
            timeLbl.Location = new Point(600, 10);
            timeLbl.AutoSize = true;
            timeLbl.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            panel1.Controls.Add(timeLbl);

            // === Таймер ===
            countdownLbl.Text = "30";
            countdownLbl.Location = new Point(730, 10);
            countdownLbl.AutoSize = true;
            countdownLbl.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            panel1.Controls.Add(countdownLbl);

            // === Кнопка старта ===
            startBtn.Text = "Start quiz";
            startBtn.Location = new Point(380, 600);
            startBtn.Size = new Size(150, 40);
            startBtn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            startBtn.BackColor = Color.LightGreen;
            startBtn.Click += StartQuiz;
            panel1.Controls.Add(startBtn);

            // === Создание примеров ===
            CreateQuestionRow(q1_1, q1_2, mark, ans1, "+", 100);
            CreateQuestionRow(q2_1, q2_2, mark2, ans2, "-", 180);
            CreateQuestionRow(q3_1, q3_2, mark3, ans3, "×", 260);

            // === Таймер ===
            quizTimer.Interval = 1000; // каждую секунду
            quizTimer.Tick += QuizTimer_Tick;
        }

        // Метод для создания одной строки примера
        private void CreateQuestionRow(Label left, Label right, Label op, TextBox ans, string symbol, int y)
        {
            left.Text = "?";
            left.Location = new Point(250, y);
            left.Size = new Size(60, 40);
            left.Font = new Font("Segoe UI", 20);
            left.TextAlign = ContentAlignment.MiddleCenter;
            
            // === Кнопка проверки ответов ===
            checkBtn.Text = "Check answers";
            checkBtn.Location = new Point(550, 600);
            checkBtn.Size = new Size(150, 40);
            checkBtn.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            checkBtn.BackColor = Color.LightBlue;
            checkBtn.Click += CheckAnswersClick;
            panel1.Controls.Add(checkBtn);


            op.Text = symbol;
            op.Location = new Point(330, y);
            op.Size = new Size(40, 40);
            op.Font = new Font("Segoe UI", 20);
            op.TextAlign = ContentAlignment.MiddleCenter;

            right.Text = "?";
            right.Location = new Point(390, y);
            right.Size = new Size(60, 40);
            right.Font = new Font("Segoe UI", 20);
            right.TextAlign = ContentAlignment.MiddleCenter;

            ans.Location = new Point(480, y + 5);
            ans.Size = new Size(100, 40);
            ans.Font = new Font("Segoe UI", 18);

            panel1.Controls.AddRange(new Control[] { left, op, right, ans });
        }

        // Запуск новой игры
        private void StartQuiz(object? sender, EventArgs e)
        {
            // Генерируем случайные числа
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

        // Обратный отсчёт таймера
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
            quizTimer.Stop(); // остановка таймера
            CheckAnswers();   // проверка ответов
        }

        // Проверка правильности
        private void CheckAnswers()
        {
            int correct = 0;

            if (int.TryParse(ans1.Text, out int a1) && a1 == i1_1 + i1_2) correct++;
            if (int.TryParse(ans2.Text, out int a2) && a2 == i2_1 - i2_2) correct++;
            if (int.TryParse(ans3.Text, out int a3) && a3 == i3_1 * i3_2) correct++;

            MessageBox.Show($"You got {correct} out of 3 correct!", "Results");
        }
    }
}
