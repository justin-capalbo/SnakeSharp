using System;
using System.Linq;
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
        public void TestCannotTurn180DegreesIfBig()
        {
            _player.Eat();
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                VerifyCannotTurnAround(dir);
            }
        }

        [TestMethod]
        public void TestCanTurn180DegreesWithNoTail()
        {
            foreach (Direction dir in Enum.GetValues(typeof(Direction)))
            {
                VerifyCanTurnAround(dir);
            }
        }

        private void VerifyCannotTurnAround(Direction startDirection)
        {
            _player.SetDirectionForced(startDirection);
            _player.TurnTowards(startDirection.Opposite());

            Assert.AreEqual(startDirection, _player.GetDirection());
        }

        private void VerifyCanTurnAround(Direction startDirection)
        {
            _player.SetDirectionForced(startDirection);
            _player.TurnTowards(startDirection.Opposite());

            Assert.AreEqual(startDirection.Opposite(), _player.GetDirection());
        }

        [TestMethod]
        public void TestAddTail()
        {
            int oldSize = _player.TailSize;
            _player.Eat();

            Assert.AreEqual(oldSize + 1, _player.TailSize);
        }

        [TestMethod]
        public void TestRemoveTail()
        {
            _player.RemoveTail();

            Assert.AreEqual(0, _player.TailSize);
        }

        [TestMethod]
        public void TestLastTailOccupiesPlayerPreviousPosition()
        {
            var prevPlayerPosition = _player.GetPosition();

            _player.Eat();
            _player.Move();

            Assert.IsTrue(prevPlayerPosition.Equals(_player.GetTail().Last()));

        }
        [TestMethod]
        public void TestBigTailFollowsHead()
        {
            _player.Eat();
            _player.Eat();
            _player.Eat();
            _player.Move();

            var tailPosition = _player.GetPosition();
            
            _player.Move();

            Assert.IsTrue(tailPosition.Equals(_player.GetTail().Last()));
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

            var expectedPosition = initialPosition + dir;

            _player.SetDirectionForced(dir);
            _player.Move();

            Assert.IsTrue(expectedPosition.Equals(_player.GetPosition()));
        }
    }
}
