using SnakeGame.Utils;

namespace SnakeGame
{
    public class SnakeGrid 
    {
        public int Width { get; }
        public int Height { get; }

        public Player Player { get; }

        public static IntVector2 DefaultPlayerPosition => new IntVector2(0,0);
        public static Direction DefaultPlayerDirection => Direction.Right;

        public SnakeGrid(int x, int y, Player p)
        {
            Width = x;
            Height = y;

            Player = p;
            ResetPlayer();
        }

        public void Step()
        {
            Player.Move();

            HandleCollision();
        }

        private void HandleCollision()
        {
            if (PlayerOutOfBounds())
                ResetPlayer();
        }

        public void ResetPlayer()
        {
            Player.SetPosition(DefaultPlayerPosition.X, DefaultPlayerPosition.Y);
            Player.SetDirection(DefaultPlayerDirection);
        }

        public bool PlayerOutOfBounds()
        {
            var playerPosition = Player.GetPosition();
            return playerPosition.X < 0 || playerPosition.X >= Width ||
                   playerPosition.Y < 0 || playerPosition.Y >= Height;
        }
    }

}
