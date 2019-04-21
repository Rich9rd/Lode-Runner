using System;

namespace LRCN
{
    public class Map
    {
        public int Height { get; }
        public int Width { get; }
        public int FrameNumber { set; get; }
        public int NumberOfCoins { set; get; }
        public int NumberOfEnemies { set; get; }
        //public Pair<int, int> PlayerPos { set; get; } // нужно // НЕ НУЖНО, В ГЕНЕРАЦИИ СОЗДАЕМ PLAYER и Enemies
        // не надо рисовать [ ] когда появятся экземпляры их классов они сами себя нарисуют, в генерации лишь создаем и задаем им координаты        
        public ViewingArea ViewingArea { set; get; }
        public Player player = new Player();
        
        public Cell[,] map = new Cell[80, 50];

        public Map(int numberOfEnemies = 0, int numberOfCoins = 0, int height = 45, int width = 45, int viewingArea = 30)
        {
            FrameNumber = 0;            
            ViewingArea = new ViewingArea(this, player);
            NumberOfCoins = numberOfCoins;
            NumberOfEnemies = numberOfEnemies;

            Height = height;
            if (height % 2 == 0)
                Height += 1;

            Width = width;
            if (width % 2 == 0)
                Width += 1;

            GenerateNewMap();
        }

        public Cell this[int i, int j]
        {
            set { map[i, j] = value; }
            get { return map[i, j]; }
        }

        public void GenerateNewMap()
        {                        
            Random rnd = new Random();
            int ran , xp, yp;
            Pair<int, int> EmptyCell;
            int square = Height * Width;
            int stairsHeight;
            //int NumberOfPits;   
            int NumberOfCoinsIter = NumberOfCoins;
            int NumberOfEnemiesIter = NumberOfEnemies;
            int NumberOfStairs = (int)(((Height - 1) / 2 - 1) * (1.2 + 0.1 * rnd.Next(10)));

            if (this.NumberOfCoins == 0)
                NumberOfCoinsIter = rnd.Next(square / 10) + 1;
            if (this.NumberOfEnemies == 0)
                NumberOfEnemiesIter = rnd.Next(square / 30) + 1;
            if (NumberOfEnemiesIter > 20)
                NumberOfEnemiesIter = 20;
            //NumberOfPits = (int)((rnd.Next(10) * 0.1 + 0.6) * ((Height + 1) / 2 - 3));

            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {
                    map[row, col] = new Cell(gameElements.Empty);
                }
            } // заполнение пустыми местами

            for (int row = 0; row < Height; row += 2)
            {
                for (int col = 0; col < Width; col++)
                {
                    map[row, col].CopyCell(Program.WallFather);
                }
            } // генерация этажей 

            for (int row = 0; row < Height; row++)
            {
                map[row, 0].CopyCell(Program.WallFather);
            }
            for (int row = 0; row < Height; row++)
            {
                map[row, Width - 1].CopyCell(Program.WallFather);
            } // генерация правой и левой стенки            

            yp = 0;
            int RetryCount = 0;
            for (int row = 3; row < Height; row += 2)
            {                
                int PreviousYStair = yp;
                ran = rnd.Next(100);
                yp = rnd.Next(Width - 2) + 1;
                stairsHeight = 2;
                if (ran > 70)
                    stairsHeight = 4;
                if (ran > 90)
                    stairsHeight = 6;
                if (stairsHeight + row >= Height - 1)
                    stairsHeight = Height - row; // чтоб лестницы не вылазили на нижнюю границу
                while (stairsHeight-- >= 0) // > 
                    map[row + stairsHeight - 1, yp].CopyCell(Program.StairFather);
                if (yp < Height - 2 && yp > 2 && rnd.Next(100) > 60) // генерация ям
                {
                    xp = rnd.Next(Width - 1) + 1;
                    while ((xp < Math.Min(yp, PreviousYStair) || xp > Math.Max(yp, PreviousYStair)) && RetryCount++ < 100)
                        xp = rnd.Next(Width - 1) + 1;
                    if (RetryCount < 99)
                        map[xp + 1, yp].CopyCell(Program.EmptyFather); ///!!! 
                }
                if (yp < Height - 2 && yp > 2 && rnd.Next(100) > 0) // генерация доп. стен
                {
                    xp = rnd.Next(Width - 1) + 1;
                    while (map[xp, yp].description != gameElements.Empty && (xp < Math.Min(yp, PreviousYStair) || xp > Math.Max(yp, PreviousYStair)) && RetryCount++ < 100)
                        xp = rnd.Next(Width - 1) + 1;
                    if (RetryCount < 99)
                        map[xp, yp].CopyCell(Program.WallFather);
                }
                NumberOfStairs--;
            } // генерация лестниц на каждом этаже, ПРОВЕРИТЬ!!!!!

            while (NumberOfStairs-- > 0)
            {
                xp = rnd.Next((Height - 2) / 2) * 2 + 2;
                yp = rnd.Next(Width - 2) + 1;
                map[xp, yp].CopyCell(Program.StairFather);
                map[xp + 1, yp].CopyCell(Program.StairFather);
            } // генерация доп. лестниц            

            while (NumberOfCoinsIter-- != 0)
            {
                EmptyCell = GetRandomEmptyCell();
                map[EmptyCell.X, EmptyCell.Y].CopyCell(Program.CoinFather);
            } // генерация монет

            while (NumberOfEnemiesIter-- > 0)
            {
                EmptyCell = GetRandomEmptyCell();
                //map[EmptyCell.X, EmptyCell.Y].CopyCell(Program.EmptyFather);
                map[EmptyCell.X, EmptyCell.Y] = new Cell(gameElements.Enemy);
            } // генерация врагов

            EmptyCell = GetRandomEmptyCell();
            map[EmptyCell.X, EmptyCell.Y].CopyCell(Program.PlayerFather);
            // спавн игрока 

            EmptyCell = GetRandomEmptyCell();
            map[EmptyCell.X, EmptyCell.Y].CopyCell(Program.ShovelFather);
            // Shovel

            EmptyCell = GetRandomEmptyCell();
            map[EmptyCell.X, EmptyCell.Y].CopyCell(Program.PickaxeFather);
            // Pickaxe


            UpdateInfo();
        }
        
        public void DrawMap(int x = 0, int y = 0)
        {
            //Console.Clear();
            Console.SetCursorPosition(x, y);
            //Console.SetCursorPosition(x, y);
            ViewingArea.MoveViewingArea(this, player);            
            for (int row = ViewingArea.Up; row <= ViewingArea.Down; row++)
            {
                for (int col = ViewingArea.Left; col <= ViewingArea.Right; col++)
                {
                    map[row, col].DrawCell();
                }                
                Console.WriteLine();
            }
            FrameNumber++;
        }
        

        public Pair<int, int> GetRandomEmptyCell()
        {
            int x = 0;
            int y = 0;
            Random rnd = new Random();
            while (map[x, y].description != gameElements.Empty)
            {
                x = rnd.Next(this.Height);
                y = rnd.Next(this.Width);
            }
            return new Pair<int, int>(x, y);
        }

        public void SpawnSuperCoin() /////////////////////////////////////////////////////////////////////
        {
            Random rnd = new Random();
            int ran = rnd.Next(100);
            Pair<int, int> EmptyCell = GetRandomEmptyCell();
            if (ran == 0)
            {

                map[EmptyCell.X, EmptyCell.Y] = new Cell(gameElements.SuperCoin); // Clone
            }
        } // in ViewingArea
        
        public void UpdateInfo()
        {
            NumberOfCoins = 0;
            NumberOfEnemies = 0;

            for (int row = 0; row < Height; row++)
            {
                for (int col = 0; col < Width; col++)
                {                    
                    switch (map[row, col].description)
                    {                        
                        case gameElements.Player:
                            player.X = row;
                            player.Y = col;
                            //ViewingArea.MoveViewingArea(this, player);
                            break;
                        case gameElements.Enemy:
                            NumberOfEnemies++;
                            break;                        
                        case gameElements.Coin:
                            NumberOfCoins++;
                            break;                        
                        default:
                            break;
                    }
                }
            }
        }
    }
}