using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab2_Convert_RGB
{
    public partial class Form1 : Form
    {
        OpenFileDialog dlg = new OpenFileDialog();
        Bitmap img2;
        int valueC = 0;

        int numC = 0;
        int numD = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            dlg.Title = "Open Image";
            dlg.Filter = "All Images|*.jpg; *.bmp; *.png";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(dlg.FileName);
            }
            dlg.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Bitmap bmp = new Bitmap(dlg.FileName);

            int width = bmp.Width;
            int height = bmp.Height;


            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixelColor = bmp.GetPixel(x, y);
                    int avg = (int)(pixelColor.R * 0.333 + pixelColor.G * 0.333 + pixelColor.B * 0.333);
                    bmp.SetPixel(x, y, Color.FromArgb(pixelColor.A, avg, avg, avg));
                }
            }
            pictureBox2.Image = bmp;
            img2 = bmp;
            /*Bitmap img = new Bitmap(bmp);
            img.Save("file.png"); //Save ภาพ*/
        
        }

        private void convertII()
        {
            //OpenFileDialog op = new OpenFileDialog();
            //op.InitialDirectory = "E:/";
            //op.Filter = "All Images|*.jpg; *.bmp; *.png";
            //op.FilterIndex = 1;

            //if (op.ShowDialog() == DialogResult.OK)
            //{
            //    pictureBox1.Image = Image.FromFile(op.FileName);
            //    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            //    pictureBox1.BorderStyle = BorderStyle.Fixed3D;
            //    Bitmap img = new Bitmap(op.FileName);
            //    StringBuilder t = new StringBuilder();
            //    int numC = 0;
            //    int numD = 0;
            //    for (int i = 0; i < img.Width; i++)
            //    {
            //        for (int j = 0; j < img.Height; j++)
            //        {
            //            t.Append((img.GetPixel(j, i).A > 100 && img.GetPixel(j, i).G > 100 &&
            //                     img.GetPixel(j, i).B > 100) ? 0 : 1);                      
            //        }

            //        t.AppendLine();                   
            //    }

            //    if (t.AppendLine().ToString() == "0")
            //    {
            //        numC += 1;
            //    }
            //    else if (t.AppendLine().ToString() == "1")
            //    {
            //        numD += 1;
            //    }

            //    label1.Text = "มีเลข 0 ทั้งหมด " + numC.ToString() + " ตัว";
            //    label2.Text = "มีเลข 1 ทั้งหมด " + numD.ToString() + " ตัว";
            //    textBox1.Text = t.AppendLine().ToString();
            //}
        }


        private void convert()
        {
            try
            {
                Bitmap bmp = new Bitmap(img2);
            int width = bmp.Width;
            int height = bmp.Height;
            string texto = "";
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixelColor = bmp.GetPixel(x, y);

                    int grey = (int)(pixelColor.R * 0.333 + pixelColor.G * 0.333 + pixelColor.B * 0.333);

                    if (grey > valueC)
                    {
                        bmp.SetPixel(x, y, Color.White);
                        numD += 1;
                        texto = texto + "1";
                    }
                    else
                    {
                        bmp.SetPixel(x, y, Color.Black);
                        numC += 1;
                        texto = texto + "0";
                    }


                }
            }

            pictureBox3.Image = bmp;
            textBox1.Text = texto;
            label1.Text = "มีสีดำทั้งหมด " + numC.ToString() + " จุด";
            label2.Text = "มีสีขาวทั้งหมด " + numD.ToString() + " จุด";
            }
            catch (Exception)
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            convert();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            label6.Text = hScrollBar1.Value.ToString();
            valueC = hScrollBar1.Value;
            if (hScrollBar1.Value > 0)
            {
                convert();
                numC = 0;
                numD = 0;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //FmHistogram ht = new FmHistogram(Image.FromFile(dlg.FileName));
            //ht.Show();
        }

        private void histogramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                FmHistogram ht = new FmHistogram(Image.FromFile(dlg.FileName));
                ht.Show();
            }
            catch (Exception)
            {

                MessageBox.Show("กรุณาเลือกภาพก่อน", "แจ้งเตือนค่ะ");
            }
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Hello I'm Wizards");
        }
    }
}
