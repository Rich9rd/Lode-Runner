using System;

namespace LRCN
{
    public class Enemy : AnimateObject, ISelfMovable
    {
        public Enemy(int X, int Y, gameElements Description = gameElements.Enemy, byte health = 2, direction direction = direction.Stop) : base(X, Y, Description, health, direction)
        {
            Description = gameElements.Enemy;
            Direction = direction.Stop;
            Health = health;
        }
        public void ChooseDirection(Map map)
        {

        }
    }
}