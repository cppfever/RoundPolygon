using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace RoundPolygon
{
    class RoundPolygon
    {
        private int m_radius = 10;
        private GraphicsPath m_path = new GraphicsPath();
        private List<Point> m_points = new List<Point>();

        public int Radius { get; set; }

        private float GetDistance(Point pt1, Point pt2)
        {
            double fD = (pt1.X - pt2.X) * (pt1.X - pt2.X) +
                        (pt1.Y - pt2.Y) * (pt1.Y - pt2.Y);
            return (float)Math.Sqrt(fD);
        }

        PointF GetLineStart(int i)
        {
            PointF pt = new PointF();
            Point pt1 = m_points[i];
            Point pt2 = m_points[(i + 1) % m_points.Count];
            float fRat = m_radius / GetDistance(pt1, pt2);
            if (fRat > 0.5f)
                fRat = 0.5f;

            pt.X = (1.0f - fRat) * pt1.X + fRat * pt2.X;
            pt.Y = (1.0f - fRat) * pt1.Y + fRat * pt2.Y;
            return pt;
        }

        PointF GetLineEnd(int i)
        {
            PointF pt = new PointF();
            Point pt1 = m_points[i];
            Point pt2 = m_points[(i + 1) % m_points.Count];
            float fRat = m_radius / GetDistance(pt1, pt2);
            if (fRat > 0.5f)
                fRat = 0.5f;

            pt.X = fRat * pt1.X + (1.0f - fRat) * pt2.X;
            pt.Y = fRat * pt1.Y + (1.0f - fRat) * pt2.Y;
            return pt;
        }

        private void AddCubicBezierFromQuadricBezier(PointF p1, PointF c, PointF p2)
        {
            PointF c1 = new PointF(p1.X + (2 * (c.X - p1.X)) / 3, p1.Y + (2 * (c.Y - p1.Y)) / 3);
            PointF c2 = new PointF(c.X + (p2.X - c.X) / 3, c.Y + (p2.Y - c.Y) / 3);
            m_path.AddBezier(p1, c1, c2, p2);
        }

        public GraphicsPath GetPath()
        {
            m_path.ClearMarkers();

            if (m_points.Count < 3)
                throw new System.Exception("Polygon should have at least 3 points!");

            PointF pt1 = new PointF();
            PointF pt2 = new PointF();
            PointF lastpoint = new PointF();

            for(int i = 0; i < m_points.Count; i++)
            {
/*                pt1 = GetLineStart(i);

                if (i == 0)
                    m_path.moveTo(pt1);
                else
                    m_path.quadTo(at(i), pt1);

                pt2 = GetLineEnd(i);
                m_path.lineTo(pt2);*/

                pt1 = GetLineStart(i);
                pt2 = GetLineEnd(i);

                if (i == 0)
                {
                    m_path.AddLine(pt1, pt2);
                    lastpoint = pt2;
                    continue;
                }
                else
                {
                    //m_path.quadTo(at(i), pt1);
                    AddCubicBezierFromQuadricBezier(lastpoint, m_points[i], pt1);
                }
                
                m_path.AddLine(pt1, pt2);
                lastpoint = pt2;
            }

            // close the last corner
            pt1 = GetLineStart(0);
            //m_path.quadTo(at(0), pt1);
            AddCubicBezierFromQuadricBezier(lastpoint, m_points[0], pt1);

            return m_path;
        }

        public RoundPolygon AddPoint(int x, int y)
        {
            m_points.Add(new Point(x, y));
            return this;
        }
    }
}
