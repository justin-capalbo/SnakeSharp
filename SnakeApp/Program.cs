using System;
using System.Linq;
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
                        player.TurnTowards(Direction.Down);
                        break;
                    case ConsoleKey.UpArrow:
                        player.TurnTowards(Direction.Up);
                        break;
                    case ConsoleKey.LeftArrow:
                        player.TurnTowards(Direction.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        player.TurnTowards(Direction.Right);
                        break;
                    case ConsoleKey.Spacebar:
                        player.Eat();
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
            var playerTail = grid.Player.GetTail();

            Console.SetCursorPosition(0, 0);
            Console.WriteLine(new string('=', X + 2));
            for (int cy = 0; cy < Y; cy++)
            {
                var row = new StringBuilder();
                row.Append(new string(' ', Y));

                foreach (var tail in playerTail)
                {
                    if (tail.Y == cy)
                        row[tail.X] = 'o';
                }

                if (playerPos.Y == cy)
                    row[playerPos.X] = 'O';

                if (grid.Apple.Y == cy)
                    row[grid.Apple.X] = '@';

                row.Insert(0, '=');
                row.Append('=');

                Console.WriteLine(row);
            }

            Console.WriteLine(new string('=', X + 2));

            Console.WriteLine($"{playerPos} {playerTail.Count()}   ");
        }
    }
}
