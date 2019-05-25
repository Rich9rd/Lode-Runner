using System;

namespace LRCN
{
    public class Player : AnimateObject
    {
        // Inventory
        public bool Pickaxe { set; get; }
        public bool Shovel { set; get; }

        public int Score { set; get; }
        
        public Player(int X = 0, int Y = 0, gameElements Description = gameElements.Player, byte health = 5, direction direction = direction.Stop, int score = 0) : base(X, Y, Description, health, direction)
        {
            Description = gameElements.Player;
            Direction = direction.Stop;
            Health = health;
            Score = 0;
        } // проверить base()

        public override gameElements CellOnThePreviousPosition(Map map, Point PreviousCell)
        {
            if (CellThatWasHere == gameElements.Coin)
                Score++;

            if (CellThatWasHere == gameElements.Enemy)            
                return gameElements.Corpse;
            if (CellThatWasHere == gameElements.Corpse)
                return gameElements.Corpse;
            if (CellThatWasHere == gameElements.Shovel)
                Shovel = true;
            if (CellThatWasHere == gameElements.Pickaxe)
                Pickaxe = true;
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
        
        public void ChooseDirection(ConsoleKey consoleKey, Map map)
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
                case ConsoleKey.Escape:
                    map.GameActive = false;
                    break;
                default:
                    Direction = direction.Stop;
                    break;
            }                        
        }

        public override void Action(Map map)
        {
            if (Console.KeyAvailable)
                ChooseDirection(Console.ReadKey(true).Key, map);
            if (Direction == direction.Down)
            {

                // удаляем нижнюю стенку
                // и помещаем в список удаленных
            }
            Move(map);
        }

        public override bool HaveShovel()
        {
            return Shovel;
        }

        public override bool HavePickaxe()
        {
            return Pickaxe;
        }

        // Shoot()

    }
}