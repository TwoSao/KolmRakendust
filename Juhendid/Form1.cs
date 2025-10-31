using System;
using System.Drawing;
using System.Windows.Forms;

namespace Juhendid
{
    // See on pildivaataja vorm. Siin saab pilte vaadata.
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

        // See funktsioon teeb pildivaataja valmis.
        public Form1()
        {
            InitializeComponent();

            // === Peaaken ===
            this.Text = "Pildivaatur";
            this.Width = 800;
            this.Height = 600;
            this.StartPosition = FormStartPosition.CenterScreen;

            // === Paneel ===
            panel1 = new Panel();
            panel1.Dock = DockStyle.Fill;
            panel1.BackColor = Color.WhiteSmoke;
            Controls.Add(panel1);

            // === Pildikast ===
            pictureBox1 = new PictureBox();
            pictureBox1.Location = new Point(50, 30);
            pictureBox1.Size = new Size(680, 380);
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            panel1.Controls.Add(pictureBox1);

            // === Info faili kohta ===
            infoLabel = new Label();
            infoLabel.Location = new Point(50, 420);
            infoLabel.Size = new Size(680, 25);
            infoLabel.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            infoLabel.Text = "Pilt pole laaditud.";
            panel1.Controls.Add(infoLabel);

            // === Märkeruut ===
            checkBox1 = new CheckBox();
            checkBox1.Text = "Venita täitmiseks";
            checkBox1.AutoSize = true;
            checkBox1.Font = new Font("Segoe UI", 10);
            checkBox1.Location = new Point(50, 450);
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            panel1.Controls.Add(checkBox1);

            // === Juhtimise nupud ===
            int topY = 480;
            int leftX = 50;

            showBtn = new Button { Text = "Näita", Size = new Size(100, 40), Location = new Point(leftX, topY) };
            showBtn.Click += showBtn_Click;

            clearBtn = new Button { Text = "Tühjenda", Size = new Size(100, 40), Location = new Point(leftX + 110, topY) };
            clearBtn.Click += clearBtn_Click;

            backgroundBtn = new Button { Text = "Taust", Size = new Size(100, 40), Location = new Point(leftX + 220, topY) };
            backgroundBtn.Click += backgroundBtn_Click;

            rotateBtn = new Button { Text = "Pööra", Size = new Size(100, 40), Location = new Point(leftX + 330, topY) };
            rotateBtn.Click += rotateBtn_Click;

            flipHBtn = new Button { Text = "Peegelda H", Size = new Size(100, 40), Location = new Point(leftX + 440, topY) };
            flipHBtn.Click += flipHBtn_Click;

            flipVBtn = new Button { Text = "Peegelda V", Size = new Size(100, 40), Location = new Point(leftX + 550, topY) };
            flipVBtn.Click += flipVBtn_Click;

            saveAsBtn = new Button { Text = "Salvesta kui", Size = new Size(100, 40), Location = new Point(leftX + 660, topY) };
            saveAsBtn.Click += saveAsBtn_Click;

            closeBtn = new Button { Text = "Sulge", Size = new Size(100, 40), Location = new Point(leftX + 660, topY) };
            closeBtn.Click += closeBtn_Click;

            panel1.Controls.AddRange(new Control[] {
                showBtn, clearBtn, backgroundBtn, rotateBtn, flipHBtn, flipVBtn, saveAsBtn, closeBtn
            });

            // === Dialoogid ===
            openFileDialog1 = new OpenFileDialog { Title = "Vali pildifail" };
            colorDialog1 = new ColorDialog();
        }

        // See funktsioon pöörab pilti.
        private void rotateBtn_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                originalImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Image = originalImage;
            }
        }

        // See funktsioon peegelda pilti horisontaalselt.
        private void flipHBtn_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                originalImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                pictureBox1.Image = originalImage;
            }
        }

        // See funktsioon peegelda pilti vertikaalselt.
        private void flipVBtn_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                originalImage.RotateFlip(RotateFlipType.RotateNoneFlipY);
                pictureBox1.Image = originalImage;
            }
        }

        // See funktsioon salvestab pildi uue nimega.
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

        // See funktsioon avab pildi failist.
        private void showBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                originalImage = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = originalImage;
                var fileInfo = new System.IO.FileInfo(openFileDialog1.FileName);
                infoLabel.Text = $"Fail: {fileInfo.Name} | Suurus: {fileInfo.Length / 1024} KB | {originalImage.Width}x{originalImage.Height}";
            }
        }

        // See funktsioon kustutab pildi ekraanilt.
        private void clearBtn_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            originalImage = null;
            infoLabel.Text = "Pilt pole laaditud.";
        }

        // See funktsioon muudab tausta värvi.
        private void backgroundBtn_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                panel1.BackColor = colorDialog1.Color;
            }
        }

        // See funktsioon muudab pildi suurust.
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = checkBox1.Checked ? PictureBoxSizeMode.StretchImage : PictureBoxSizeMode.Zoom;
        }

        // See funktsioon sulgeb programmi.
        private void closeBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
