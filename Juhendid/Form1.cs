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
        Button flipHBtn;
        Button flipVBtn;
        Button saveAsBtn;

        Image originalImage;

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
            int leftX = 50;

            showBtn = new Button { Text = "Show", Size = new Size(100, 40), Location = new Point(leftX, topY) };
            showBtn.Click += showBtn_Click;

            clearBtn = new Button { Text = "Clear", Size = new Size(100, 40), Location = new Point(leftX + 110, topY) };
            clearBtn.Click += clearBtn_Click;

            backgroundBtn = new Button { Text = "Background", Size = new Size(100, 40), Location = new Point(leftX + 220, topY) };
            backgroundBtn.Click += backgroundBtn_Click;

            rotateBtn = new Button { Text = "Rotate", Size = new Size(100, 40), Location = new Point(leftX + 330, topY) };
            rotateBtn.Click += rotateBtn_Click;

            flipHBtn = new Button { Text = "Flip H", Size = new Size(100, 40), Location = new Point(leftX + 440, topY) };
            flipHBtn.Click += flipHBtn_Click;

            flipVBtn = new Button { Text = "Flip V", Size = new Size(100, 40), Location = new Point(leftX + 550, topY) };
            flipVBtn.Click += flipVBtn_Click;

            saveAsBtn = new Button { Text = "Save As", Size = new Size(100, 40), Location = new Point(leftX + 660, topY) };
            saveAsBtn.Click += saveAsBtn_Click;

            closeBtn = new Button { Text = "Close", Size = new Size(100, 40), Location = new Point(leftX + 770, topY) };
            closeBtn.Click += closeBtn_Click;

            panel1.Controls.AddRange(new Control[] {
                showBtn, clearBtn, backgroundBtn, rotateBtn, flipHBtn, flipVBtn, saveAsBtn, closeBtn
            });

            // === Диалоги ===
            openFileDialog1 = new OpenFileDialog { Title = "Select a picture file" };
            colorDialog1 = new ColorDialog();
        }

        // === Rotate ===
        private void rotateBtn_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                originalImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Image = originalImage;
            }
        }

        // === Flip Horizontal ===
        private void flipHBtn_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                originalImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox1.Image = originalImage;
            }
        }

        // === Flip Vertical ===
        private void flipVBtn_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                originalImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                pictureBox1.Image = originalImage;
            }
        }

        // === Save As ===
        private void saveAsBtn_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PNG Image|*.png|JPEG Image|*.jpg";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    pictureBox1.Image.Save(sfd.FileName);
                }
            }
        }

        // === Show image ===
        private void showBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                originalImage = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = originalImage;
                var fileInfo = new System.IO.FileInfo(openFileDialog1.FileName);
                infoLabel.Text = $"File: {fileInfo.Name} | Size: {fileInfo.Length / 1024} KB | {originalImage.Width}x{originalImage.Height}";
            }
        }

        // === Clear ===
        private void clearBtn_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            originalImage = null;
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
