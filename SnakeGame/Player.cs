using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SnakeGame.Utils;

namespace SnakeGame
{
    public class Player 
    {
        private IntVector2 _position = new IntVector2(0,0); 
        private Direction _direction = Direction.Right;

        private readonly Queue<IntVector2> _tail;
        private int _tailGrowth = 0;

        public int TailSize => _tail.Count + _tailGrowth;

        public Player()
        {
            _tail = new Queue<IntVector2>();
        }

        public void SetPosition(int x, int y)
        {
            _position = new IntVector2(x, y);
        }

        public IntVector2 GetPosition()
        {
            return _position;
        }

        public bool IsCollidingWithSelf()
        {
            return _tail.Any(t => t.Equals(_position));
        }

        public void Eat()
        {
            _tailGrowth++;
        }

        public void RemoveTail()
        {
            _tail.Clear();
        }

        public IEnumerable<IntVector2> GetTail()
        {
            return _tail;
        }

        public Direction GetDirection()
        {
            return _direction;
        }

        /// <summary>
        /// Used by game controls to face a certain direction.  Forbids 180 degree turns when player has a tail.
        /// </summary>
        /// <param name="dir"></param>
        public void TurnTowards(Direction dir)
        {
            if (TailSize > 0 && dir == _direction.Opposite())
                return;

            SetDirectionForced(dir);
        }

        public void SetDirectionForced(Direction dir)
        {
            _direction = dir;
        }

        public void Move()
        {
            //Store previous position and move player
            var prevPosition = _position;
            _position += _direction;

            //Add new tail segments
            if (_tailGrowth > 0)
                GrowTailAt(prevPosition);
            else
            {
                //Move end of tail to player's head
                if (_tail.Any())
                {
                    FollowPlayerWithTail(prevPosition);
                }
            }
        }

        private void GrowTailAt(IntVector2 pos)
        {
            _tail.Enqueue(new IntVector2(pos));
            _tailGrowth--;
        }


        private void FollowPlayerWithTail(IntVector2 pos)
        {
            var tailEnd = _tail.Dequeue();
            tailEnd.Set(pos);
            _tail.Enqueue(tailEnd);
        }
    }
}
