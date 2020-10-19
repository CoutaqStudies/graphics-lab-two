using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Сделал Меликов Михаил ИВТ-1 8 вариант
namespace GraphicsLabTwo
{
    public partial class Form1 : Form
    {
        Graphics g;
        public Form1()
        {
            InitializeComponent();
            g = pictureBox1.CreateGraphics();
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void DrawLines(float penSize, float width, float height)
        {
            var pen = new Pen(Color.Crimson, penSize);
            g.DrawRectangle(pen, 0, 0, width, height);
            g.DrawLine(pen, 0, height / 2, width, height / 2);
            g.DrawLine(pen, width / 2, height, width / 2, -height);
        }
        private void DrawFunction(float penSize)
        {
            float widthMultiplier, heightMultiplier;
            var width = pictureBox1.Width-1;
            var height = pictureBox1.Height-1;
            Clear();
            switch (g.PageUnit)
            {
                case GraphicsUnit.Millimeter:
                    widthMultiplier = g.DpiX / 25.4f;
                    heightMultiplier = g.DpiY / 25.4f;
                    break;
                case GraphicsUnit.Inch:
                    widthMultiplier = g.DpiX;
                    heightMultiplier = g.DpiY;
                    break;
                default:
                    widthMultiplier = 1f;
                    heightMultiplier = 1f;
                    break;
            }
            DrawLines(penSize, (width-1)/widthMultiplier, (height-1)/heightMultiplier);
            var pen = new Pen(Color.LimeGreen, penSize);

            int dex = 0, dey = 0;
            double x = -Math.PI * 2;
            for (int ex = 0; ex <= width/widthMultiplier; ex++)
            {
                int y = (int)(-function(x)*200+250);
                int ey = (int)(y/heightMultiplier);
                g.DrawLine(pen, dex, dey, ex, ey);
                dex = ex; dey = ey;
                x = x + (Math.PI * 4) / (width/widthMultiplier);
            }
        }
        private double function(double x)
        {
            return (4 * Math.Pow(x, 2) + 3 * x);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            g.Clear(Color.FromArgb(240,248,255));
        }

        private void buttonPixels_Click(object sender, EventArgs e)
        {
            g.PageUnit = GraphicsUnit.Pixel;
            DrawFunction(1f);
        }

        private void buttonMm_Click(object sender, EventArgs e)
        {
            g.PageUnit = GraphicsUnit.Millimeter;
            DrawFunction(0.1f);
        }

        private void buttonInches_Click(object sender, EventArgs e)
        {
            g.PageUnit = GraphicsUnit.Inch;
            DrawFunction(0.005f);
        }
    }
}