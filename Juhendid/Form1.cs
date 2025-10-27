using System;
using System.Drawing;
using System.Windows.Forms;

namespace Juhendid
{
    public partial class Form1 : Form
    {
        Panel panel1;
        PictureBox pictureBox1;
        Label infoLabel;
        CheckBox checkBox1;

        Button showBtn;
        Button clearBtn;
        Button closeBtn;
        Button backgroundBtn;
        Button rotateBtn;
        Button zoomInBtn;
        Button zoomOutBtn;
        Button optionsBtn;

        float zoomFactor = 1.0f;

        OpenFileDialog openFileDialog1;
        ColorDialog colorDialog1;

        public Form1()
        {
            InitializeComponent();

            // === Главное окно ===
            this.Text = "Picture Viewer";
            this.Width = 900;
            this.Height = 700;
            this.StartPosition = FormStartPosition.CenterScreen;

            // === Панель ===
            panel1 = new Panel();
            panel1.Dock = DockStyle.Fill;
            panel1.BackColor = Color.WhiteSmoke;
            Controls.Add(panel1);

            // === PictureBox ===
            pictureBox1 = new PictureBox();
            pictureBox1.Location = new Point(50, 30);
            pictureBox1.Size = new Size(780, 480);
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            panel1.Controls.Add(pictureBox1);

            // === Информация о файле ===
            infoLabel = new Label();
            infoLabel.Location = new Point(50, 520);
            infoLabel.Size = new Size(780, 25);
            infoLabel.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            infoLabel.Text = "No image loaded.";
            panel1.Controls.Add(infoLabel);

            // === CheckBox ===
            checkBox1 = new CheckBox();
            checkBox1.Text = "Stretch to fill";
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 10);
            checkBox1.Location = new Point(50, 555);
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            panel1.Controls.Add(checkBox1);

            // === Кнопки управления ===
            int topY = 590;
            int btnW = 110;
            int btnH = 40;
            int spacing = 10;
            int leftX = 50;

            showBtn = CreateStyledButton("Show", leftX, topY, showBtn_Click);
            clearBtn = CreateStyledButton("Clear", leftX + (btnW + spacing), topY, clearBtn_Click);
            backgroundBtn = CreateStyledButton("Background", leftX + (btnW + spacing) * 2, topY, backgroundBtn_Click);
            rotateBtn = CreateStyledButton("Rotate", leftX + (btnW + spacing) * 3, topY, rotateBtn_Click);
            zoomInBtn = CreateStyledButton("Zoom +", leftX + (btnW + spacing) * 4, topY, zoomInBtn_Click);
            zoomOutBtn = CreateStyledButton("Zoom -", leftX + (btnW + spacing) * 5, topY, zoomOutBtn_Click);
            closeBtn = CreateStyledButton("Close", leftX + (btnW + spacing) * 7, topY, closeBtn_Click);

            panel1.Controls.AddRange(new Control[] {
                showBtn, clearBtn, backgroundBtn, rotateBtn, zoomInBtn, zoomOutBtn, optionsBtn, closeBtn
            });

            // === Диалоги ===
            openFileDialog1 = new OpenFileDialog { Title = "Select a picture file" };
            colorDialog1 = new ColorDialog();
        }

        // === Создание кнопки в стиле Form2 ===
        private Button CreateStyledButton(string text, int x, int y, EventHandler clickHandler)
        {
            Button btn = new Button();
            btn.Text = text;
            btn.Size = new Size(110, 40);
            btn.Location = new Point(x, y);
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.LightGreen;
            btn.Cursor = Cursors.Hand;
            btn.Click += clickHandler;
            return btn;
        }

        // === Zoom ===
        private void zoomInBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                zoomFactor += 0.1f;
                pictureBox1.Scale(new SizeF(1.1f, 1.1f));
            }
        }

        private void zoomOutBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null && zoomFactor > 0.3f)
            {
                zoomFactor -= 0.1f;
                pictureBox1.SizeMode += 1;
            }
        }

        

        // === Rotate ===
        private void rotateBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Refresh();
            }
        }

        // === Show image ===
        private void showBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                var fileInfo = new System.IO.FileInfo(openFileDialog1.FileName);
                infoLabel.Text = $"File: {fileInfo.Name} | Size: {fileInfo.Length / 1024} KB | {pictureBox1.Image.Width}x{pictureBox1.Image.Height}";
            }
        }

        // === Clear ===
        private void clearBtn_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            infoLabel.Text = "No image loaded.";
        }

        // === Background color ===
        private void backgroundBtn_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                panel1.BackColor = colorDialog1.Color;
            }
        }

        // === CheckBox ===
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = checkBox1.Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.Zoom;
        }

        // === Close ===
        private void closeBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
