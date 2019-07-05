using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{

    public class MathForAlgebra
    {
        public static LinearParameterFunction getParameterFunction(Point A, Point B)
        {
            return new LinearParameterFunction()
            {
                pointOnFunction = A,
                direction = new Point() { X = B.X - A.X, Y = B.Y - A.Y }
            };
        }

        public static Tuple<double, double> getPointOnFunction(LinearParameterFunction g, double parameter)
        {
            return new Tuple<double, double>(g.pointOnFunction.X + parameter * g.direction.X, g.pointOnFunction.Y + parameter * g.direction.Y);
        }

        public static Point getCutPointFromPointToLine(LinearParameterFunction g1, LinearParameterFunction g2)
        {
            if (g1.direction.Y == 0)
            {
                double mu = (double)(g1.pointOnFunction.Y - g2.pointOnFunction.Y) / g2.direction.Y;
                Tuple<double,double>PointAsTuple =  getPointOnFunction(g2, mu);
                return new Point() { X = (int)PointAsTuple.Item1, Y = (int)PointAsTuple.Item2 };
            }
            else
            {
                double l = (double)(g1.pointOnFunction.X - g2.pointOnFunction.X) / g2.direction.X;
                Tuple<double, double> PointAsTuple = getPointOnFunction(g2, l);
                return new Point() { X = (int)PointAsTuple.Item1, Y = (int)PointAsTuple.Item2 };
            }
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class LinearParameterFunction
    {
        public Point pointOnFunction { get; set; }
        public Point direction { get; set; }
    }

    public class Rect
    {
        public Point downLeft { get; set; }
        public Point upperLeft { get; set; }
        public Point upperRight { get; set; }
        public Point downRight { get; set; }

        public LinearParameterFunction leftLine { get; set; }
        public LinearParameterFunction rightLine { get; set; }
        public LinearParameterFunction upperLine { get; set; }
        public LinearParameterFunction downLine { get; set; }

        public bool isPointInRect(Point P)
        {
            Point cutPLeftSide = MathForAlgebra.getCutPointFromPointToLine(
                new LinearParameterFunction()
                {
                    pointOnFunction = P,
                    direction = new Point() { X = -1, Y = 0 }
                },
                leftLine);

            Point cutPRightSide = MathForAlgebra.getCutPointFromPointToLine(
                new LinearParameterFunction()
                {
                    pointOnFunction = P,
                    direction = new Point() { X = 1, Y = 0 }
                },
                rightLine);

            Point cutPUpperSide = MathForAlgebra.getCutPointFromPointToLine(
                new LinearParameterFunction()
                {
                    pointOnFunction = P,
                    direction = new Point() { X = 0, Y = 1 }
                },
                upperLine);

            Point cutPDownSide = MathForAlgebra.getCutPointFromPointToLine(
                new LinearParameterFunction()
                {
                    pointOnFunction = P,
                    direction = new Point() { X = 0, Y = -1 }
                },
                downLine);

            if (P.X >= cutPLeftSide.X && P.X <= cutPRightSide.X && P.Y <= cutPUpperSide.Y && P.Y >= cutPDownSide.Y)
                return true;

            return false;
        }
        public List<Point> getAllPointsInRect()
        {
            Point maxLeftPoint = downLeft.X < upperLeft.X ? downLeft : upperLeft;
            Point maxTopPoint = upperLeft.Y > upperRight.Y ? upperLeft : upperRight;
            Point maxRightPoint = upperRight.X > downRight.X ? upperRight : downRight;
            Point maxBottomPoint = downRight.Y < downLeft.Y ? downRight : downLeft;

            List<Point> pointsInRect = new List<Point>();

            for (int x_coordinate = maxLeftPoint.X; x_coordinate <= maxRightPoint.X; x_coordinate++)
            {
                for (int y_coordinate = maxBottomPoint.Y; y_coordinate <= maxTopPoint.Y; y_coordinate++)
                {
                    Point currentPoint = new Point() { X = x_coordinate, Y = y_coordinate };
                    if (isPointInRect(currentPoint))
                    {
                        pointsInRect.Add(currentPoint);
                    }
                }
            }

            return pointsInRect;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Zum Ausprobieren - - 
            List<Point> P = new List<Point>()
            {
                new Point() { X = 1, Y = 1 },
                new Point() { X = 4, Y = 7 },
                new Point() {X = 11, Y = 7 },
                new Point() {X = 8, Y = 1 }
            };
            Rect Test = set_Rect_Points(P);

            Debug.WriteLine("DOWNLEFT: " + Test.downLeft.X + "/" + Test.downLeft.Y);
            Debug.WriteLine("UPPERLEFT: " + Test.upperLeft.X + "/" + Test.upperLeft.Y);
            Debug.WriteLine("DOWNRIGHT: " + Test.downRight.X + "/" + Test.downRight.Y);
            Debug.WriteLine("UPPERRIGHT: " + Test.upperRight.X + "/" + Test.upperRight.Y);

            Point inR = new Point() { X = 7, Y = 3 };
            Point outR = new Point() { X = 1, Y = 2 };

            Debug.WriteLine("LEFT: " + Test.leftLine.direction.X + "/" + Test.leftLine.direction.Y);
            Debug.WriteLine("TOP: " + Test.upperLine.direction.X + "/" + Test.upperLine.direction.Y);
            Debug.WriteLine("RIGHT: " + Test.rightLine.direction.X + "/" + Test.rightLine.direction.Y);
            Debug.WriteLine("BOTTOM: " + Test.downLine.direction.X + "/" + Test.downLine.direction.Y);


            Debug.WriteLine("P(" + inR.X + "/" + inR.Y + ") ist in Rect: " + Test.isPointInRect(inR));
            Debug.WriteLine("P(" + outR.X + "/" + outR.Y + ") ist in Rect: " + Test.isPointInRect(outR));

            List<Point> pInR = Test.getAllPointsInRect();
            foreach (Point Q in pInR)
            {
                Debug.WriteLine("P(" + Q.X + "/" + Q.Y + ") ");
            }

        }

        // Please Just a List with 4 Points!!!
        public static Rect set_Rect_Points(List<Point> Points)
        {
            List<Point> RightLeftSortedPoints = Points.OrderBy(point => point.X).ToList();
            List<Point> LeftPoints = RightLeftSortedPoints.GetRange(0, 2);
            List<Point> RightPoints = RightLeftSortedPoints.GetRange(2, 2);

            List<Point> upperDownSortedByLeftPoints = LeftPoints.OrderBy(point => point.Y).ToList();
            List<Point> upperDownSortedByRightPoints = RightPoints.OrderBy(point => point.Y).ToList();

            Point downLeft = upperDownSortedByLeftPoints[0];
            Point upperLeft = upperDownSortedByLeftPoints[1];
            Point downRight = upperDownSortedByRightPoints[0];
            Point upperRight = upperDownSortedByRightPoints[1];

            return new Rect()
            {
                downLeft = downLeft,
                upperLeft = upperLeft,
                downRight = downRight,
                upperRight = upperRight,

                leftLine = MathForAlgebra.getParameterFunction(downLeft, upperLeft),
                upperLine = MathForAlgebra.getParameterFunction(upperLeft, upperRight),
                rightLine = MathForAlgebra.getParameterFunction(upperRight, downRight),
                downLine = MathForAlgebra.getParameterFunction(downRight, downLeft)
            };
        }


    }
}
