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
            Point nextCell = new Point(X, Y);

            if (!Falling(map)) // Nextcell -> current  FALLING!!!!!!!!!!!!!!!!!!!!!!!
            {
                switch (Direction)
                {
                    case direction.Stop:
                        break;
                    case direction.Right:
                        if (HavePickaxe())
                        {
                            if (map[X, Y + 1].description == gameElements.Wall)
                            {
                                map.listOfDestroyedWalls.AddWall(new DestroyedWall(true, X, Y + 1, map.FrameNumber));
                                map[X, Y + 1].CopyCell(Program.EmptyFather);
                            }                            
                        }
                        if (canMoveToTheNextCell(new Point(X, Y + 1)))
                           nextCell.Y++;                         
                        break;
                    case direction.Left:
                        if (HavePickaxe())
                        {
                            if (map[X, Y - 1].description == gameElements.Wall)
                            {
                                map.listOfDestroyedWalls.AddWall(new DestroyedWall(true, X, Y - 1, map.FrameNumber));
                                map[X, Y - 1].CopyCell(Program.EmptyFather);
                            }                            
                        }
                        if (canMoveToTheNextCell(new Point(X, Y - 1)))
                           nextCell.Y--;                         
                        break;
                    case direction.Up:
                        if (canMoveToTheNextCell(new Point(X - 1, Y)))                        
                            nextCell.X--;                         
                        break;
                    case direction.Down:
                        if (canMoveToTheNextCell(new Point(X + 1, Y)))                                                    
                            nextCell.X++;
                        else
                        {
                            if (HaveShovel())
                            {
                                if (map[X + 1, Y].description == gameElements.Wall) // копаем
                                {
                                    map.listOfDestroyedWalls.AddWall(new DestroyedWall(true, X + 1, Y, map.FrameNumber));
                                    map[X + 1, Y].CopyCell(Program.EmptyFather);
                                }
                            }                            
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (canMoveToTheNextCell(new Point(X + 1, Y)))                
                    nextCell.X++;
                    
                else
                    if (!inMap(nextCell)) // упал за карту
                        Health = 0;                                                     
            }
            
            
            if (X != nextCell.X || Y != nextCell.Y) // передвинулись
            {
                map[X, Y] = new Cell(CellOnThePreviousPosition(map, new Point(X, Y))); // отрисовуем то, что здесь было                                    
                CellThatWasHere = map[nextCell.X, nextCell.Y].description; // новый символ на предыдущей клетке
                X = nextCell.X;
                Y = nextCell.Y;
                map[X, Y] = new Cell(Description)
                {
                    symbol = CharDependingOnDirection(Direction, map)
                };
            }
            
            Direction = direction.Stop;


            bool canMoveToTheNextCell(Point NextCell)
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

            bool inMap(Point ChekedCell)
            {
                if (ChekedCell.X >= map.Height || ChekedCell.X < 0 || ChekedCell.Y >= map.Width || ChekedCell.Y < 0)
                    return false;
                else
                    return true;
            }
        }
        


        public virtual gameElements CellOnThePreviousPosition(Map map, Point PreviousCell)
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
            if (X + 1 >= map.Height)
            {
                map.GameActive = false;
                return false;
            }
            return map[X + 1, Y].description != gameElements.Wall &&
                   map[X + 1, Y].description != gameElements.Stair &&
                   map[X + 1, Y].description != gameElements.Bomb; // объекты по которым можно ходить
        }

        public virtual bool HaveShovel()
        {
            return false;
        }

        public virtual bool HavePickaxe()
        {
            return false;
        }
    }
}
