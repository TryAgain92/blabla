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
            //QUICKSTART - - 
            List<Point> P = new List<Point>()
            {
                new Point() { X = 1, Y = 1 },
                new Point() { X = 4, Y = 7 },
                new Point() {X = 11, Y = 7 },
                new Point() {X = 8, Y = 1 }
            };


            List<Point> pInR = Test.getAllPointsInRect();
            foreach (Point Q in pInR)
            {
                Debug.WriteLine("P(" + Q.X + "/" + Q.Y + ") ");
            }

        }
        
         public static bool pointIsInRect(Point P, List<Point> cornerPoints)
        {
            List<Point> RightLeftSortedPoints = cornerPoints.OrderBy(point => point.X).ToList();
            List<Point> LeftPoints = RightLeftSortedPoints.GetRange(0, 2);
            List<Point> RightPoints = RightLeftSortedPoints.GetRange(2, 2);

            List<Point> upperDownSortedByLeftPoints = LeftPoints.OrderBy(point => point.Y).ToList();
            List<Point> upperDownSortedByRightPoints = RightPoints.OrderBy(point => point.Y).ToList();

            Point bottomLeft = upperDownSortedByLeftPoints[0];
            Point topLeft = upperDownSortedByLeftPoints[1];
            Point bottomRight = upperDownSortedByRightPoints[0];
            Point topRight = upperDownSortedByRightPoints[1];

            List<Point> leftLine = new List<Point>() { bottomLeft, new Point() { X = topLeft.X - bottomLeft.X, Y = topLeft.Y - bottomLeft.Y } };
            List<Point> topLine = new List<Point>() { topLeft, new Point() { X = topRight.X - topLeft.X, Y = topRight.Y - topLeft.Y } };
            List<Point> rightLine = new List<Point>() { topRight, new Point() { X = bottomRight.X - topRight.X, Y = bottomRight.Y - topRight.Y } };
            List<Point> bottomLine = new List<Point>() { bottomRight, new Point() { X = bottomLeft.X - bottomRight.X, Y = bottomLeft.Y - bottomRight.Y } };


            double parameterPointToLeftSide = (double)(P.Y - leftLine[0].Y) / leftLine[1].Y;
            Tuple<double, double> shortestPointOnLeftLineToPointAsDouble = new Tuple<double, double>(
                    leftLine[0].X + parameterPointToLeftSide * leftLine[1].X,
                    leftLine[0].Y + parameterPointToLeftSide * leftLine[1].Y
                );
            Point shortestPointOnLeftLineToPoint = new Point() { X = (int)shortestPointOnLeftLineToPointAsDouble.Item1, Y = (int)shortestPointOnLeftLineToPointAsDouble.Item2 };


            double parameterPointToTopSide = (double)(P.X - topLine[0].X) / topLine[1].X;
            Tuple<double, double> shortestPointOnTopLineToPointAsDouble = new Tuple<double, double>(
                    topLine[0].X + parameterPointToTopSide * topLine[1].X,
                    topLine[0].Y + parameterPointToTopSide * topLine[1].Y
                );
            Point shortestPointOnTopLineToPoint = new Point() { X = (int)shortestPointOnTopLineToPointAsDouble.Item1, Y = (int)shortestPointOnTopLineToPointAsDouble.Item2 };


            double parameterPointToRightSide = (double)(P.Y - rightLine[0].Y) / rightLine[1].Y;
            Tuple<double, double> shortestPointOnRightLineToPointAsDouble = new Tuple<double, double>(
                    rightLine[0].X + parameterPointToRightSide * rightLine[1].X,
                    rightLine[0].Y + parameterPointToRightSide * rightLine[1].Y
                );
            Point shortestPointOnRightLineToPoint = new Point() { X = (int)shortestPointOnRightLineToPointAsDouble.Item1, Y = (int)shortestPointOnRightLineToPointAsDouble.Item2 };


            double parameterPointToBottomSide = (double)(P.X - rightLine[0].X) / rightLine[1].X;
            Tuple<double, double> shortestPointOnBottomLineToPointAsDouble = new Tuple<double, double>(
                    rightLine[0].X + parameterPointToBottomSide * rightLine[1].X,
                    rightLine[0].Y + parameterPointToBottomSide * rightLine[1].Y
                );
            Point shortestPointOnBottomLineToPoint = new Point() { X = (int)shortestPointOnBottomLineToPointAsDouble.Item1, Y = (int)shortestPointOnBottomLineToPointAsDouble.Item2 };

            if (P.X >= shortestPointOnLeftLineToPoint.X && P.X <= shortestPointOnRightLineToPoint.X && P.Y <= shortestPointOnTopLineToPoint.Y && P.Y >= shortestPointOnBottomLineToPoint.Y)
                return true;

            return false;
        }


        public static List<Point> getPointsFromRect(List<Point> cornerPoints)
        {
            List<Point> RightLeftSortedPoints = cornerPoints.OrderBy(point => point.X).ToList();
            List<Point> LeftPoints = RightLeftSortedPoints.GetRange(0, 2);
            List<Point> RightPoints = RightLeftSortedPoints.GetRange(2, 2);

            List<Point> upperDownSortedByLeftPoints = LeftPoints.OrderBy(point => point.Y).ToList();
            List<Point> upperDownSortedByRightPoints = RightPoints.OrderBy(point => point.Y).ToList();

            Point bottomLeft = upperDownSortedByLeftPoints[0];
            Point topLeft = upperDownSortedByLeftPoints[1];
            Point bottomRight = upperDownSortedByRightPoints[0];
            Point topRight = upperDownSortedByRightPoints[1];

            Point maxLeftPoint = bottomLeft.X < topLeft.X ? bottomLeft : topLeft;
            Point maxTopPoint = topLeft.Y > bottomRight.Y ? topLeft : bottomRight;
            Point maxRightPoint = topRight.X > bottomRight.X ? topRight : bottomRight;
            Point maxBottomPoint = bottomRight.Y < bottomLeft.Y ? bottomRight : bottomLeft;

            List<Point> pointsInRect = new List<Point>();

            for (int x_coordinate = maxLeftPoint.X; x_coordinate <= maxRightPoint.X; x_coordinate++)
            {
                for (int y_coordinate = maxBottomPoint.Y; y_coordinate <= maxTopPoint.Y; y_coordinate++)
                {
                    Point currentPoint = new Point() { X = x_coordinate, Y = y_coordinate };
                    if (pointIsInRect(currentPoint, cornerPoints))
                    {
                        pointsInRect.Add(currentPoint);
                    }
                }
            }

            return pointsInRect;
        }


    }
}
