using System;
using System.Threading;

namespace LRCN
{
    class Program
    {
        public static Cell WallFather = new Cell(gameElements.Wall);
        public static Cell CoinFather = new Cell(gameElements.Coin);
        public static Cell TrapFather = new Cell(gameElements.Trap);
        public static Cell BombFather = new Cell(gameElements.Bomb);
        public static Cell StairFather = new Cell(gameElements.Stair);
        public static Cell EnemyFather = new Cell(gameElements.Enemy);
        public static Cell EmptyFather = new Cell(gameElements.Empty);
        public static Cell PlayerFather = new Cell(gameElements.Player);
        public static Cell ShovelFather = new Cell(gameElements.Shovel);
        public static Cell PickaxeFather = new Cell(gameElements.Pickaxe);
        public static Cell TeleportFather = new Cell(gameElements.Teleport);        

        static void Main(string[] args)
        {
            Console.CursorVisible = false;            
            Map map = new Map();            
            map.DrawMap();            
            while (map.player.Health > 0)
            {
                map.player.Action(map);
                map.DrawMap();
                // очиска места
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine(map.player.Score);
                Console.WriteLine(map.FrameNumber);
            }
            
            Console.ReadKey();
        }
        
        static void Action(ICanInteract Object, Map map) // добавить список живых элементов
        {
            Object.Hit(map);
        }

        public static void CharacterOutput(string str)
        {
            bool skip = false;            
            foreach (char ch in str)
            {
                skip = Console.KeyAvailable;
                Console.Write(ch);                
                if (!skip)
                    Thread.Sleep(50);
            }
        }

        
    }
}
