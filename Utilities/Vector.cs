namespace AoC2025.Utilities
{
    public class Vec2(Int64 x, Int64 y) : Tuple<Int64, Int64>(x, y)
    {
        public Int64 X => Item1;
        public Int64 Y => Item2;

        public Int64 SquareDist(Vec2 other)
        {
            return (Int64)(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }
    }

    public class Vec3(Int64 x, Int64 y, Int64 z) : Tuple<Int64, Int64, Int64>(x, y, z)
    {
        public Int64 X => Item1;
        public Int64 Y => Item2;
        public Int64 Z => Item3;

        public Int64 SquareDist(Vec3 other)
        {
            return (Int64)(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2) + Math.Pow(Z - other.Z, 2));
        }
    }
}
