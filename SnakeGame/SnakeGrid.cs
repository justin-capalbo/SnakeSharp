using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using SnakeGame.Utils;

namespace SnakeGame
{
    public class SnakeGrid
    {
        public int Width { get; }
        public int Height { get; }

        private IntVector2 _player; 
        private Direction _playerDirection;

        public SnakeGrid(int x, int y)
        {
            Width = x;
            Height = y;
            ResetPlayer();
        }

        public void Step()
        {
            MovePlayer(_playerDirection);
        }

        public void ResetPlayer()
        {
            PositionPlayer(0, 0);
            SetPlayerDirection(Direction.Right);
        }

        public void SetPlayerDirection(Direction dir)
        {
            _playerDirection = dir;
        }

        public void MovePlayer(Direction d)
        {
            _player += d.UnitVector();
        }

        public void PositionPlayer(int x, int y)
        {
            _player = new IntVector2(x, y);
        }

        public IntVector2 GetPlayerPosition()
        {
            return _player;
        }

        public bool PlayerOutOfBounds()
        {
            return _player.X < 0 || _player.X >= Width ||
                   _player.Y < 0 || _player.Y >= Height;
        }
    }

}
