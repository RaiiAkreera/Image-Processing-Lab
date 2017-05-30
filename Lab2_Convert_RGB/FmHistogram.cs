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
    public partial class FmHistogram : Form
    {

        Image image = null;
        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        public FmHistogram(Image image)
        {
            InitializeComponent();
            this.image = image;
        }
        Xuly img;
        Bitmap bm;

        private void FmHistogram_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = image;
            bm = (Bitmap)(image);
            img = new Xuly(bm);
            pictureBox1.Image = (Image)(img.GetBitmap());
            pictureBox3.Image = (Image)(img.GetHistogram(pictureBox3));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            img.Equalization();
            pictureBox2.Image = (Image)(img.GetBitmap());
            pictureBox4.Image = (Image)(img.GetHistogram(pictureBox4));
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
