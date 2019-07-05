using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
         

        }
        
        public static List<Point> getRotatedPoints(List<Point> points, Point center, double angleOfRotation )
        {
            List<Point> rotatedPoints = new List<Point>();
            foreach (Point p in points)
            {
                double radiusOfCurrentRotation = Math.Pow(Math.Pow(p.X - center.X, 2) + Math.Pow(p.Y - center.Y, 2), 0.5);
                double currentAngle = Math.Acos((double)(p.X - center.X) / radiusOfCurrentRotation); //In Radians

                Point rotatedPoint = new Point() {
                    X = (int)(center.X + Math.Cos(currentAngle + angleOfRotation * Math.PI / 180) * radiusOfCurrentRotation),
                    Y = (int)(center.Y + Math.Sin(currentAngle + angleOfRotation * Math.PI / 180) * radiusOfCurrentRotation)
                };

                rotatedPoints.Add(rotatedPoint);
            }

            return rotatedPoints;
        }


    }
}
