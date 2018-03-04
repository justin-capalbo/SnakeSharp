using System;
using SnakeGame.Utils;

namespace SnakeGame
{
    public class SnakeGrid 
    {
        public int Width { get; }
        public int Height { get; }

        private Player Player { get; }

        public static IntVector2 DefaultPlayerPosition => new IntVector2(0,0);
        public static Direction DefaultPlayerDirection => Direction.Right;

        public IntVector2 Apple { get; private set; }

        public SnakeGrid(int x, int y, Player p)
        {
            Width = x;
            Height = y;

            Player = p;

            ResetAll();
        }

        public void ResetAll()
        {
            ResetPlayer();
            ResetApple();
        }

        public void Step()
        {
            Player.Move();

            HandleCollision();
        }

        private void HandleCollision()
        {
            if (PlayerOutOfBounds() || Player.IsCollidingWithSelf())
            {
                ResetAll();
            }

            if (Player.GetPosition().Equals(Apple))
            {
                Player.Eat();
                ResetApple();
            }
        }

        private void ResetPlayer()
        {
            Player.SetPosition(DefaultPlayerPosition.X, DefaultPlayerPosition.Y);
            Player.SetDirectionForced(DefaultPlayerDirection);
            Player.RemoveTail();
        }

        private void ResetApple()
        {
            var r = new Random();
            SetApple(r.Next(Width), r.Next(Height));
        }

        public void SetApple(int x, int y)
        {
            Apple = new IntVector2(x, y);
        }

        public bool PlayerOutOfBounds()
        {
            var playerPosition = Player.GetPosition();
            return playerPosition.X < 0 || playerPosition.X >= Width ||
                   playerPosition.Y < 0 || playerPosition.Y >= Height;
        }
    }

}
