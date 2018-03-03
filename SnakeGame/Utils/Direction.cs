using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeGame.Utils
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class DirectionExtensions
    {
        public static IntVector2 UnitVector(this Direction self)
        {
            if (self == Direction.Up)
                return new IntVector2(0, 1);
            else if (self == Direction.Down) 
                return new IntVector2(0, -1);
            else if (self == Direction.Left) 
                return new IntVector2(-1, 0);
            else if (self == Direction.Right) 
                return new IntVector2(1, 0);
            
            return null;
        }
    }

}
