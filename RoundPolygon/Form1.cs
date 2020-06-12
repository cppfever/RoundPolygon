using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RoundPolygon
{
    public partial class Form1 : Form
    {
        RoundPolygon polygon = new RoundPolygon();

        public Form1()
        {
            InitializeComponent();

            polygon.AddPoint(147, 187).AddPoint(95, 187)
              .AddPoint(100, 175).AddPoint(145, 165).AddPoint(140, 95)
              .AddPoint(5, 85).AddPoint(5, 70).AddPoint(140, 70).AddPoint(135, 45)
              .AddPoint(138, 25).AddPoint(145, 5).AddPoint(155, 5).AddPoint(162, 25)
              .AddPoint(165, 45).AddPoint(160, 70).AddPoint(295, 70).AddPoint(295, 85)
              .AddPoint(160, 95).AddPoint(155, 165).AddPoint(200, 175)
                  .AddPoint(205, 187).AddPoint(153, 187).AddPoint(150, 199);

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {            
            GraphicsPath path = polygon.GetPath();
            e.Graphics.DrawPath(new Pen(Color.Red, 2), path);
        }
    }
}
