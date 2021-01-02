namespace mu_labda.algorithm
{
    public class MyPoint2D
    {
        private double functionValue;
        private double x;
        private double y;

        public MyPoint2D()
        {
            x = 0;
            y = 0;
            functionValue = -999;
        }

        public MyPoint2D(double x, double y)
        {
            this.x = x;
            this.y = y;
            functionValue = SinusFunctionChartLogic.Function(x, y);
        }

        public double GetX()
        {
            return x;
        }

        public void SetX(double x)
        {
            this.x = x;
        }

        public double GetY()
        {
            return y;
        }

        public void SetY(double y)
        {
            this.y = y;
        }

        public double GetFunctionValue()
        {
            return functionValue;
        }

        public void SetFunctionValue(double functionValue)
        {
            this.functionValue = functionValue;
        }
    }
}