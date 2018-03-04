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

        private static SnakeGrid _grid;
        private static Player _player;

        private static SnakeGrid MakeStandardGrid()
        {
            _player = new Player();
            return new SnakeGrid(StandardGridWidth, StandardGridHeight, _player);
        } 

        [TestInitialize]
        public void Initialize()
        {
            _grid = MakeStandardGrid();
        }

        [TestMethod]
        public void TestGridDimensions()
        {
            Assert.AreEqual(StandardGridWidth, _grid.Width);
            Assert.AreEqual(StandardGridHeight, _grid.Height);
        }

        [TestMethod]
        public void TestGridStartsWithPlayerAtOrigin()
        {
            var expectedPosition = SnakeGrid.DefaultPlayerPosition;
            var playerPosition = _player.GetPosition();
            
            Assert.AreEqual(expectedPosition.X, playerPosition.X);
            Assert.AreEqual(expectedPosition.Y, playerPosition.Y);
        }

        [TestMethod]
        public void TestMovingOutOfBoundsResetsPlayer()
        {
            var expectedPosition = SnakeGrid.DefaultPlayerPosition;
            var expectedDirection = SnakeGrid.DefaultPlayerDirection;

            _player.SetPosition(0, MinOobY);
            _player.Eat();
            _grid.Step();
            
            Assert.IsTrue(expectedPosition.Equals(_player.GetPosition()));
            Assert.IsTrue(expectedDirection.Equals(_player.GetDirection()));
            Assert.AreEqual(0, _player.TailSize);
        }

        [TestMethod]
        public void TestPositionPlayerInbounds()
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    _player.SetPosition(x, y); 

                    Assert.IsFalse(_grid.PlayerOutOfBounds());
                }
            }
        }

        [TestMethod]
        public void TestPositionPlayerOutOfBounds()
        {
            _player.SetPosition(MinOobX, 0);
            Assert.IsTrue(_grid.PlayerOutOfBounds());

            _player.SetPosition(0, MinOobY); 
            Assert.IsTrue(_grid.PlayerOutOfBounds());

            _player.SetPosition(_grid.Width, 0); 
            Assert.IsTrue(_grid.PlayerOutOfBounds());

            _player.SetPosition(0, _grid.Height); 
            Assert.IsTrue(_grid.PlayerOutOfBounds());
        }

        [TestMethod]
        public void TestAppleGrowsPlayer()
        {
            var expectedTailSize = _player.TailSize + 1;
            PlaceAppleInFrontOfPlayer();

            _grid.Step();

            Assert.AreEqual(expectedTailSize, _player.TailSize);
        }

        [TestMethod]
        public void TestPlayerCollidingSelfResetsPlayer()
        {
            var expectedPosition = SnakeGrid.DefaultPlayerPosition;

            for (int i = 0; i < 5; i++)
            {
                PlaceAppleInFrontOfPlayer();
                _grid.Step();
            }

            WalkInACircle();

            Assert.AreEqual(0, _player.TailSize);
            Assert.IsTrue(expectedPosition.Equals(_player.GetPosition()));
        }

        private void WalkInACircle()
        {
            _player.TurnTowards(Direction.Down);
            _grid.Step();
            _player.TurnTowards(Direction.Left);
            _grid.Step();
            _player.TurnTowards(Direction.Up);
            _grid.Step();
        }

        private void PlaceAppleInFrontOfPlayer()
        {
            var applePosition = _player.GetPosition() + _player.GetDirection();
            _grid.SetApple(applePosition.X, applePosition.Y);
        }

    }
}
