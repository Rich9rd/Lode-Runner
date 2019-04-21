using System;

namespace LRCN
{
    public class Player : AnimateObject
    {
        // bool isFirstPlayer?  color depending on it    Blue & Green
        public int Score { set; get; }

        // Inventory

        public Player(int X = 0, int Y = 0, gameElements Description = gameElements.Player, byte health = 5, direction direction = direction.Stop, int score = 0) : base(X, Y, Description, health, direction)
        {
            Description = gameElements.Player;
            Direction = direction.Stop;
            Health = health;
            Score = 0;
        } // проверить base()

        public override gameElements CellOnThePreviousPosition(Map map, Pair<int, int> PreviousCell)
        {
            if (CellThatWasHere == gameElements.Coin)
                Score++;

            if (CellThatWasHere == gameElements.Enemy)            
                return gameElements.Corpse;
            if (CellThatWasHere == gameElements.Corpse)
                return gameElements.Corpse;
            // особенные символы

            if (CellThatWasHere == gameElements.Stair) // элементы которые не стираются после того, как игрок по ним пройдет 
                return CellThatWasHere;            
            else
                return gameElements.Empty;            
        }

        public override char CharDependingOnDirection(direction direction, Map map)
        {
            if (Falling(map))
                return 'v';
            
            switch (direction)
            {
                case direction.Stop:
                    return 'I';                    
                case direction.Right:
                    return '>';
                case direction.Left:
                    return '<';
                case direction.Up:
                    return '^';
                case direction.Down:
                    return 'v';                    
                default:
                    return 'I';                    
            }            
        }
        
        public void ChooseDirection(ConsoleKey consoleKey)
        {            
            switch (consoleKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    Direction = direction.Up;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    Direction = direction.Down;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    Direction = direction.Right;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    Direction = direction.Left;
                    break;
                default:
                    Direction = direction.Stop;
                    break;
            }                        
        }

        public override void Action(Map map)
        {
            if (Console.KeyAvailable)
                ChooseDirection(Console.ReadKey(true).Key);
            Move(map);
        }

        // Shoot()
        // new Bullet // стрелять в притык, проверять на чем "стоит" пуля
    }
}