using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeGame;
using SnakeGame.Utils;

namespace SnakeTest
{
    [TestClass]
    public class TestGrid
    {
        private const int StandardGridWidth = 30, StandardGridHeight = 30;
        private const int MinOobX = -1, MinOobY = -1;

        private static SnakeGrid grid;
        private static Player player;

        private static SnakeGrid MakeStandardGrid()
        {
            player = new Player();
            return new SnakeGrid(StandardGridWidth, StandardGridHeight, player);
        } 

        [TestInitialize]
        public void Initialize()
        {
            grid = MakeStandardGrid();
            player = grid.Player;
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
            var expectedPosition = SnakeGrid.DefaultPlayerPosition;
            var playerPosition = player.GetPosition();
            
            Assert.AreEqual(expectedPosition.X, playerPosition.X);
            Assert.AreEqual(expectedPosition.Y, playerPosition.Y);
        }

        [TestMethod]
        public void TestMovingOutOfBoundsResetsPlayer()
        {
            var expectedPosition = SnakeGrid.DefaultPlayerPosition;
            var expectedDirection = SnakeGrid.DefaultPlayerDirection;

            player.SetPosition(0, MinOobY);
            player.Eat();
            grid.Step();
            
            Assert.IsTrue(expectedPosition.Equals(player.GetPosition()));
            Assert.IsTrue(expectedDirection.Equals(player.GetDirection()));
            Assert.AreEqual(0, player.TailSize);
        }

        [TestMethod]
        public void TestPositionPlayerInbounds()
        {
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    player.SetPosition(x, y); 

                    Assert.IsFalse(grid.PlayerOutOfBounds());
                }
            }
        }

        [TestMethod]
        public void TestPositionPlayerOutOfBounds()
        {
            player.SetPosition(MinOobX, 0);
            Assert.IsTrue(grid.PlayerOutOfBounds());

            player.SetPosition(0, MinOobY); 
            Assert.IsTrue(grid.PlayerOutOfBounds());

            player.SetPosition(grid.Width, 0); 
            Assert.IsTrue(grid.PlayerOutOfBounds());

            player.SetPosition(0, grid.Height); 
            Assert.IsTrue(grid.PlayerOutOfBounds());
        }

        [TestMethod]
        public void TestAppleGrowsPlayer()
        {
            var expectedTailSize = player.TailSize + 1;
            PlaceAppleInFrontOfPlayer();

            grid.Step();

            Assert.AreEqual(expectedTailSize, player.TailSize);
        }

        [TestMethod]
        public void TestPlayerCollidingSelfResetsPlayer()
        {
            var expectedPosition = SnakeGrid.DefaultPlayerPosition;

            for (int i = 0; i < 5; i++)
            {
                PlaceAppleInFrontOfPlayer();
                grid.Step();
            }

            WalkInACircle();

            Assert.AreEqual(0, player.TailSize);
            Assert.IsTrue(expectedPosition.Equals(player.GetPosition()));
        }

        private void WalkInACircle()
        {
            player.TurnTowards(Direction.Down);
            grid.Step();
            player.TurnTowards(Direction.Left);
            grid.Step();
            player.TurnTowards(Direction.Up);
            grid.Step();
        }

        private void PlaceAppleInFrontOfPlayer()
        {
            var applePosition = player.GetPosition() + player.GetDirection();
            grid.SetApple(applePosition.X, applePosition.Y);
        }

    }
}
