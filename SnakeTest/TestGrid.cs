using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeGame;

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

            player.SetPosition(0, MinOobY);
            grid.Step();
            
            Assert.IsTrue(expectedPosition.Equals(player.GetPosition()));
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

    }
}
