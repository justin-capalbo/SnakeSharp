using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SnakeGame;
using SnakeGame.Utils;

namespace SnakeTest
{
    [TestClass]
    public class TestPlayer
    {
        private static Player _player;

        [TestInitialize]
        public void Initialize()
        {
            _player = new Player();
        }

        [TestMethod]
        public void TestSetPosition()
        {
            Random r = new Random();
            for (int i = 0; i < 100; i++)
            {
                var newPosition = new IntVector2(r.Next(), r.Next());
                _player.SetPosition(newPosition.X, newPosition.Y);

                Assert.IsTrue(newPosition.Equals(_player.GetPosition()));
            }
        }

        [TestMethod]
        public void TestAddTail()
        {
            int oldSize = _player.Size;
            _player.Grow();

            Assert.AreEqual(oldSize + 1, _player.Size);
        }

        [TestMethod]
        public void TestRemoveTail()
        {
            _player.RemoveTail();
            Assert.AreEqual(1, _player.Size);
        }

        [TestMethod]
        public void TestStepMovesPlayerAllDimensions()
        {
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                VerifyMoveAdjustsPosition(dir);
            }
        }

        public void VerifyMoveAdjustsPosition(Direction dir)
        {
            var initialPosition = new IntVector2(5,5);
            _player.SetPosition(initialPosition.X, initialPosition.Y);

            var expectedPosition = initialPosition + dir.UnitVector();

            _player.SetDirection(dir);
            _player.Move();

            Assert.IsTrue(expectedPosition.Equals(_player.GetPosition()));
        }
    }
}
