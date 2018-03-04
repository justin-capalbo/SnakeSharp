using System;

namespace SnakeGame.Utils
{
    public enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }

    public class DirectionException : Exception
    {
        public DirectionException(Direction dir) : base($"Invalid direction given ({dir}).")
        {
        }
    }

    public static class DirectionExtensions
    {
        public static Direction Opposite(this Direction self)
        {
            switch (self)
            {
                case Direction.Down:
                    return Direction.Up; 
                case Direction.Up:
                    return Direction.Down; 
                case Direction.Left:
                    return Direction.Right; 
                case Direction.Right:
                    return Direction.Left;
                default:
                    throw new DirectionException(self);
            }
        }

        public static IntVector2 UnitVector(this Direction self)
        {
            switch (self)
            {
                case Direction.Down:
                    return new IntVector2(0, 1);
                case Direction.Up:
                    return new IntVector2(0, -1);
                case Direction.Left:
                    return new IntVector2(-1, 0);
                case Direction.Right:
                    return new IntVector2(1, 0);
                default:
                    throw new DirectionException(self);
            }
        }
    }

}
