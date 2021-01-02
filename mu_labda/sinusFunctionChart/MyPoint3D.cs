namespace mu_labda
{
    public class MyPoint3D
    {
        private readonly int x;
        private readonly int y;
        private readonly double z;

        public MyPoint3D(int x, int y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public double GetZ()
        {
            return z;
        }
    }
}