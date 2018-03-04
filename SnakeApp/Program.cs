using System;
using System.Text;
using SnakeGame;
using System.Threading;
using SnakeGame.Utils;

namespace SnakeApp
{
    class Program
    {
        private static Timer timer;
        private static SnakeGrid grid;

        public static void Main(string[] args)
        {
            Console.CursorVisible = false;

            StartGame();
        }

        private static void Tick(object state)
        {
            grid.Step();
            Render(grid);
        }

        public static void StartGame()
        {
            var player = new Player();
            grid = new SnakeGrid(20, 20, player);
            timer = new Timer(Tick, null, 0, 200);

            bool gameRunning = true;
            while (gameRunning)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.DownArrow:
                        player.SetDirection(Direction.Down);
                        break;
                    case ConsoleKey.UpArrow:
                        player.SetDirection(Direction.Up);
                        break;
                    case ConsoleKey.LeftArrow:
                        player.SetDirection(Direction.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        player.SetDirection(Direction.Right);
                        break;
                    case ConsoleKey.Escape:
                        gameRunning = false;
                        break;
                }

            }
        }

        public static void Render(SnakeGrid grid)
        {
            int X = grid.Width;
            int Y = grid.Height;

            var playerPos = grid.Player.GetPosition();

            Console.SetCursorPosition(0, 0);
            Console.WriteLine(new string('=', X + 2));
            for (int cy = 0; cy < Y; cy++)
            {
                var row = new StringBuilder();
                row.Append(new string(' ', Y));

                if (playerPos.Y == cy)
                    row[playerPos.X] = 'O';

                row.Insert(0, '=');
                row.Append('=');

                Console.WriteLine(row);
            }

            Console.WriteLine(new string('=', X + 2));

            Console.WriteLine($"{playerPos}   ");
        }
    }
}
