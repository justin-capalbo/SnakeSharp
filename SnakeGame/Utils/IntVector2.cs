namespace SnakeGame.Utils
{
    public class IntVector2
    {
        public int X;
        public int Y;

        public IntVector2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public bool Equals(IntVector2 v2)
        {
            return X == v2.X && this.Y == v2.Y;
        }

        public static IntVector2 operator +(IntVector2 v1, IntVector2 v2)
        {
            return new IntVector2(v1.X + v2.X, v1.Y + v2.Y);
        }

    }
}
