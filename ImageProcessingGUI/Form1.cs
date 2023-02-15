using System.Drawing;

namespace ImageProcessingGUI
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.InitialDirectory = @"D:\ITS\S2\(PCD) Pengolahan Citra Digital\GrayscaleImages";
            openFileDialog1.Title = "Browse Black and White image";
            openFileDialog1.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                Bitmap bitmap = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = bitmap;
                pictureBox1.Width = pictureBox1.Image.Width;
                pictureBox1.Height = pictureBox1.Image.Height;

                int[] grayscales = this.GetGrayscalesCount(bitmap);

                int totalPixel = bitmap.Width * bitmap.Height;

                int[] targetGrayscale = this.getTargetGrayscale(grayscales, totalPixel);

                Bitmap newBitmap = generateNewBitmap(bitmap, targetGrayscale);

                pictureBox2.Image = newBitmap;
                pictureBox2.Width = newBitmap.Width;
                pictureBox2.Height = newBitmap.Height;

            }
        }

        private Bitmap generateNewBitmap(Bitmap bitmap, int[] targetGrayscale)
        {
            Bitmap newBitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), bitmap.PixelFormat);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    int grayscaleValue = bitmap.GetPixel(x, y).R;
                    int target = targetGrayscale[grayscaleValue];

                    newBitmap.SetPixel(x, y, Color.FromArgb(255, target, target, target));
                }
            }

            return newBitmap;
        }
        private int[] getTargetGrayscale(int[] grayscales, int totalPixel)
        {
            int[] targetGrayscale = Enumerable.Repeat(-1, 256).ToArray();
            float cummulativeSum = 0;

            for (int i = 0; i < 256; i++)
            {
                int numberOfGrayscale = grayscales[i];
                if (numberOfGrayscale == 0) continue;
                cummulativeSum += numberOfGrayscale;
                float variance = cummulativeSum / totalPixel;
                float target = variance * 255;
                targetGrayscale[i] = Convert.ToInt32(target);
            }
            return targetGrayscale;
        }

        private int[] GetGrayscalesCount(Bitmap bitmap)
        {
            int[] grayscales = Enumerable.Repeat(0, 256).ToArray();

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    int grayscaleValue = bitmap.GetPixel(x, y).R;
                    grayscales[grayscaleValue]++;
                }
            }
            return grayscales;
        }

        private void changeBitmapGrayscaleValue(Bitmap bitmap, int grayscale)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}