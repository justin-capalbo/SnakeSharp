using System;
using System.Drawing;
using System.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeGame;
using SnakeGame.Utils;
using Moq;

namespace SnakeTest
{
    [TestClass]
    public class TestGrid
    {
        private const int StandardGridWidth = 30;
        private const int StandardGridHeight = 30;

        private static SnakeGrid grid;

        private static SnakeGrid StandardGrid() => new SnakeGrid(StandardGridWidth, StandardGridHeight);

        [TestInitialize]
        public void Initialize()
        {
            grid = StandardGrid();
        }

        [TestMethod]
        public void TestGridDimensions()
        {
            Assert.AreEqual(StandardGridWidth, grid.Width);
            Assert.AreEqual(StandardGridHeight, grid.Height);
        }

        [TestMethod]
        public void TestGridStartsWithPlayerAtOrigin()
        {
            var expectedPosition = new IntVector2(0, 0);
            var playerPosition = grid.GetPlayerPosition();
            
            Assert.AreEqual(expectedPosition.X, playerPosition.X);
            Assert.AreEqual(expectedPosition.Y, playerPosition.Y);
        }

        [TestMethod]
        public void TestPositionPlayerInbounds()
        {
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    grid.SetPlayerPosition(x, y); 

                    Assert.IsFalse(grid.PlayerOutOfBounds());
                }
            }
        }

        [TestMethod]
        public void TestPositionPlayerOutOfBounds()
        {
            grid.SetPlayerPosition(-1, 0);
            Assert.IsTrue(grid.PlayerOutOfBounds());

            grid.SetPlayerPosition(0, -1); 
            Assert.IsTrue(grid.PlayerOutOfBounds());

            grid.SetPlayerPosition(grid.Width, 0); 
            Assert.IsTrue(grid.PlayerOutOfBounds());

            grid.SetPlayerPosition(0, grid.Height); 
            Assert.IsTrue(grid.PlayerOutOfBounds());
        }

        [TestMethod]
        public void TestCannotAddPlayerOutOfBounds()
        {
            grid.ResetPlayer();

            Assert.IsFalse(grid.PlayerOutOfBounds());
        }

        [TestMethod]
        public void TestStepMovesPlayerAllDimensions()
        {
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                VerifyStepInDirectionMovesPlayer(dir);
            }
        }

        public void VerifyStepInDirectionMovesPlayer(Direction dir)
        {
            var playerPosition = new IntVector2(5,5);
            grid.SetPlayerPosition(playerPosition.X, playerPosition.Y);

            var expectedPosition = playerPosition + dir.UnitVector();

            grid.SetPlayerDirection(dir);
            grid.Step();

            Assert.IsTrue(expectedPosition.Equals(grid.GetPlayerPosition()));
        }

    }
}
