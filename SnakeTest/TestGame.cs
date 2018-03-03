using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeGame;

namespace SnakeTest
{
    [TestClass]
    public class TestGame
    {
        private const int StandardGridWidth = 30;
        private const int StandardGridHeight = 30;

        [TestMethod]
        public void TestGridDimensions()
        {
            var grid = StandardGrid();

            Assert.AreEqual(StandardGridWidth, grid.Length);
            Assert.AreEqual(StandardGridHeight, grid.Height);
        }

        [TestMethod]
        public void TestAddingPlayer()
        {
            var grid = StandardGrid();

            grid.AddPlayer();

            Point playerPosition = grid.GetPlayerPosition();

            VerifyPlayerIsInBounds(playerPosition);
        }


        [TestMethod]
        public void TestCannotAddPlayerOutOfBounds()
        {
            var grid = StandardGrid();

            grid.AddPlayer();

            Assert.IsFalse(grid.PlayerOutOfBounds());
        }

        private void VerifyPlayerIsInBounds(Point p)
        {
            Assert.IsTrue(p.X > 0 && p.X < grid.Width && 
                          p.Y > 0 && p.Y < grid.Height);
        }

        private static SnakeGrid StandardGrid()
        {
            return new SnakeGrid(StandardGridWidth, StandardGridHeight);
        }
    }
}
