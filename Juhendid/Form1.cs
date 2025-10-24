using System;
using System.Drawing;
using System.Windows.Forms;

namespace Juhendid
{
    public partial class Form1 : Form
    {
        Button showBtn;
        Button clearBtn;
        Button closeBtn;
        Button backgroundBtn;
        Button optionsBtn;

        Panel panel1;

        OpenFileDialog openFileDialog1;
        ColorDialog colorDialog1;

        CheckBox checkBox1;
        PictureBox pictureBox1;
        

        public Form1()
        {
            InitializeComponent();

            this.Text = "Picture Viewer";
            this.Width = 700;
            this.Height = 500;
            this.StartPosition = FormStartPosition.CenterScreen;

            panel1 = new Panel();
            panel1.Width = 700;
            panel1.Height = 500;
            panel1.Location = new Point(0, 0);
            panel1.BackColor = Color.White;
            Controls.Add(panel1);

            // === PictureBox (основное место под изображение) ===
            pictureBox1 = new PictureBox();
            pictureBox1.Location = new Point(10, 10);
            pictureBox1.Size = new Size(670, 350);
            pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            panel1.Controls.Add(pictureBox1);

            // === CheckBox ===
            checkBox1 = new CheckBox();
            checkBox1.Text = "Turn on fill";
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(20, 380);
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            panel1.Controls.Add(checkBox1);

            showBtn = new Button();
            showBtn.Text = "Show";
            showBtn.Size = new Size(100, 40);
            showBtn.Location = new Point(20, 420);
            showBtn.Click += showBtn_Click;
            panel1.Controls.Add(showBtn);

            clearBtn = new Button();
            clearBtn.Text = "Clear";
            clearBtn.Size = new Size(100, 40);
            clearBtn.Location = new Point(140, 420);
            clearBtn.Click += clearBtn_Click;
            panel1.Controls.Add(clearBtn);

            backgroundBtn = new Button();
            backgroundBtn.Text = "Background";
            backgroundBtn.Size = new Size(120, 40);
            backgroundBtn.Location = new Point(260, 420);
            backgroundBtn.Click += backgroundBtn_Click;
            panel1.Controls.Add(backgroundBtn);

            closeBtn = new Button();
            closeBtn.Text = "Close";
            closeBtn.Size = new Size(100, 40);
            closeBtn.Location = new Point(400, 420);
            closeBtn.Click += closeBtn_Click;
            panel1.Controls.Add(closeBtn);

            
            optionsBtn = new Button();
            optionsBtn.Text = "Options";
            optionsBtn.Size = new Size(100, 40);
            optionsBtn.Location = new Point(520, 420);
            optionsBtn.Click += optionsBtn_Click;
            panel1.Controls.Add(optionsBtn);
            
            
            openFileDialog1 = new OpenFileDialog
            {
                Title = "Select a picture file",
            };
            colorDialog1 = new ColorDialog();

            
        }
        
        private void optionsBtn_Click(object sender, EventArgs e)
        {
            CustomForm customMessage = new CustomForm(
                "Options",
                "Choose option",
                "Inversion",
                "Filter",
                "",
                "Vali ise!"
            );
            customMessage.StartPosition = FormStartPosition.CenterParent;
            customMessage.ShowDialog();

        }
        private void showBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }
        
        private void clearBtn_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }
        
        private void backgroundBtn_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                panel1.BackColor = colorDialog1.Color;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else
            {
                pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
            }
        }
        private void closeBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

      
    }
}
