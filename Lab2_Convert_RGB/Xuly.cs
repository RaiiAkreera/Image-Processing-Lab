using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace Lab2_Convert_RGB
{
    class Xuly
    {

        private byte[,] matrix;
        public int w, h;
        public int[] his;
        public Xuly(Bitmap bm)
        {
            Rectangle rec = new Rectangle(0, 0, bm.Width, bm.Height);
            BitmapData bdata = bm.LockBits(rec, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int du = bdata.Stride - 3 * bm.Width;
            matrix = new byte[bm.Height, bm.Width];
            w = bm.Width;
            h = bm.Height;

            unsafe
            {
                byte* p = (byte*)(bdata.Scan0);
                for (int i = 0; i < bm.Height; i++)
                {
                    for (int j = 0; j < bm.Width; j++)
                    {
                        matrix[i, j] = (byte)(0.114 * p[0] + 0.587 * p[1] + 0.299 * p[2]);
                        p += 3;
                    }
                    p += du;
                }
            }
            bm.UnlockBits(bdata);
        }

        public Bitmap GetBitmap()
        {
            Bitmap bm = new Bitmap(w, h);
            Rectangle rec = new Rectangle(0, 0, bm.Width, bm.Height);
            BitmapData bdata = bm.LockBits(rec, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int du = bdata.Stride - 3 * bm.Width;

            unsafe
            {
                byte* p = (byte*)(bdata.Scan0);
                for (int i = 0; i < bm.Height; i++)
                {
                    for (int j = 0; j < bm.Width; j++)
                    {
                        p[0] = p[1] = p[2] = matrix[i, j];
                        p += 3;
                    }
                    p += du;
                }
            }
            bm.UnlockBits(bdata);
            return bm;
        }

        public Bitmap GetHistogram(PictureBox pic)
        {
            his = new int[256];
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    his[matrix[i, j]]++;
                }
            }
            Pen p = new Pen(Color.Red, 1.0f);
            Bitmap bm = new Bitmap(pic.Width, pic.Height);
            Graphics gr = Graphics.FromImage(bm);

            for (int i = 0; i < 256; i++)
            {
                gr.DrawLine(p, i, pic.Height, i, pic.Height - his[i] / 100);
            }
            return bm;
        }

        public double[] tansuat()
        {
            double[] ts = new double[his.Length];
            for (int i = 0; i < his.Length; i++)
            {
                ts[i] = (double)his[i] / ((w * h));
            }
            double[] z = new double[256];
            z[0] = ts[0];
            for (int i = 1; i < 256; i++)
            {
                z[i] = z[i - 1] + ts[i];
            }
            return z;
        }

        public void Equalization()
        {
            double[] ts = tansuat();
            int gray;
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    gray = (int)Math.Round(ts[matrix[i, j]] * 255);
                    matrix[i, j] = (byte)gray;
                }
            }
        }

    }
}
