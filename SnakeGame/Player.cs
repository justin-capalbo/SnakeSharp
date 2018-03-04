using SnakeGame.Utils;

namespace SnakeGame
{
    public class Player 
    {
        private IntVector2 _position; 
        private Direction _direction;

        public void SetDirection(Direction dir)
        {
            _direction = dir;
        }

        public void Move()
        {
            _position += _direction.UnitVector();
        }

        public void SetPosition(int x, int y)
        {
            _position = new IntVector2(x, y);
        }

        public IntVector2 GetPosition()
        {
            return _position;
        }
    }
}
