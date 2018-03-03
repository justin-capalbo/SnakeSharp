using System;
using SnakeGame;

namespace SnakeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new SnakeGrid(30, 30);

            grid.Step();
            grid.Step();
            grid.Step();
            grid.Step();
        }
    }
}
