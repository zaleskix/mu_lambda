namespace mu_labda
{
    public class My3DPoint
    {
        private int x;
        private int y;
        private double z;

        public My3DPoint(int x, int y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public int GetX()
        {
            return this.x;
        }

        public int GetY()
        {
            return this.y;
        }

        public double GetZ()
        {
            return this.z;
        }
    }
}