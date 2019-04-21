using System;

namespace LRCN
{
    abstract public class GameObject : ICanInteract
    {        
        public gameElements Description { set; get; }        
        public int X { set; get; }
        public int Y { set; get; }

        public GameObject(int x, int y, gameElements description)
        {            
            X = x;
            Y = y;
            Description = description;
        }

        public virtual void Action(Map map) { }

        public virtual void Hit(Map map) { }
    }

    abstract public class InanimateObjects : GameObject
    {
        public InanimateObjects(int X, int Y, gameElements Description) : base(X, Y, Description) { }
    }

    abstract public class AnimateObject : GameObject
    {
        public direction Direction { set; get; }
        public gameElements CellThatWasHere { set; get; }
        public byte Health { set; get; }
        public AnimateObject(int X, int Y, gameElements Description, byte health, direction direction) : base(X, Y, Description)
        {
            CellThatWasHere = gameElements.Empty;
            Health = health;
            Direction = direction;
        }

        public void Move(Map map) // .private tempPair
        {
            Pair<int, int> nextCell = new Pair<int, int>(X, Y);

            if (!Falling(map)) // Nextcell -> current  FALLING!!!!!!!!!!!!!!!!!!!!!!!
            {
                switch (Direction)
                {
                    case direction.Stop:
                        break;
                    case direction.Right:
                        if (canMoveToTheNextCell(new Pair<int, int>(X, Y + 1)))
                           nextCell.Y++;                         
                        break;
                    case direction.Left:
                        if (canMoveToTheNextCell(new Pair<int, int>(X, Y - 1)))
                           nextCell.Y--;                         
                        break;
                    case direction.Up:
                        if (canMoveToTheNextCell(new Pair<int, int>(X - 1, Y)))                        
                            nextCell.X--;                         
                        break;
                    case direction.Down:
                        if (canMoveToTheNextCell(new Pair<int, int>(X + 1, Y)))                                                    
                            nextCell.X++;                            
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (canMoveToTheNextCell(new Pair<int, int>(X + 1, Y)))                
                    nextCell.X++;
                    
                else
                    if (!inMap(nextCell)) // упал за карту
                        Health = 0;                                                     
            }
            
            
            if (X != nextCell.X || Y != nextCell.Y) // передвинулись
            {
                map[X, Y] = new Cell(CellOnThePreviousPosition(map, new Pair<int, int>(X, Y))); // отрисовуем то, что здесь было                                    
                CellThatWasHere = map[nextCell.X, nextCell.Y].description; // новый символ на предыдущей клетке
                X = nextCell.X;
                Y = nextCell.Y;
                map[X, Y] = new Cell(Description)
                {
                    symbol = CharDependingOnDirection(Direction, map)
                };
            }
            
            Direction = direction.Stop;


            bool canMoveToTheNextCell(Pair<int, int> NextCell)
            {
                if (!inMap(NextCell))
                    return false;

                if (map[NextCell.X, NextCell.Y].description != gameElements.Wall &&
                    map[NextCell.X, NextCell.Y].description != gameElements.Bomb
                    ) // элементы через которые нельзя пройти
                    return true;
                else
                    return false;
            }

            bool inMap(Pair<int, int> ChekedCell)
            {
                if (ChekedCell.X >= map.Height || ChekedCell.X < 0 || ChekedCell.Y >= map.Width || ChekedCell.Y < 0)
                    return false;
                else
                    return true;
            }
        }
        


        public virtual gameElements CellOnThePreviousPosition(Map map, Pair<int, int> PreviousCell)
        {
            return CellThatWasHere;
        }

        public virtual char CharDependingOnDirection(direction direction, Map map)
        {
            return 'I';
        }

        public override void Hit(Map map)
        {
            Health--;
            if (Health <= 0)
            {
                map[X, Y] = new Cell(gameElements.Corpse);
            } // +++++ удалить из списка живых
        }

        public bool Falling(Map map)
        {
            if (CellThatWasHere == gameElements.Stair)
                return false;
            //if (X + 1 >= map.Height)
            //    return false;              // альтернатива падению за карту
            return map[X + 1, Y].description != gameElements.Wall &&
                   map[X + 1, Y].description != gameElements.Stair &&
                   map[X + 1, Y].description != gameElements.Bomb; // объекты по которым можно ходить
        }
    }
}
