using System;
using System.IO;
using System.Threading;
using System.Xml.Serialization;

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
            Console.SetWindowSize(100, 40);
            Console.Title = "Lode Runnner                     (для завершения игры нажмите \"Esc\")";

            Console.Write("To dig get a shovel:  ");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine("S ");
            Console.BackgroundColor = ConsoleColor.Black;
            CharacterOutput("Hello, enter your name: ");
            string PlayerName = Console.ReadLine();            
            
            Map map = new Map();            
            map.DrawMap();            
            while (map.player.Health > 0 && map.GameActive)
            {                
                map.player.Action(map);
                map.DrawMap();
                // очиска места
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("{1}, your score: {0}/{2}", map.player.Score, PlayerName, map.NumberOfCoins);                
                if (map.NumberOfCoins == map.player.Score)
                    break;
            }

            Console.WriteLine("Game Over, your score: {0}", map.player.Score);
            User user = new User(map.player.Score, PlayerName);
            XmlSerializer formatter = new XmlSerializer(typeof(TableOfRecords));
            TableOfRecords tableOfRecords = new TableOfRecords();
            try
            {
                using (FileStream fs = new FileStream("Records.xml", FileMode.OpenOrCreate))
                {
                    tableOfRecords = (TableOfRecords)formatter.Deserialize(fs);                    
                }
            }
            catch (Exception)
            {
                  
            }
            tableOfRecords.IsNewRecord = false;
            tableOfRecords.Add(user);
            
            using (FileStream fs = new FileStream("Records.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, tableOfRecords);
            }

            if (tableOfRecords.IsNewRecord)
            {
                Congratulations(map.player.Score);
            }            
            
            Console.WriteLine("-----------------Table Of Records-----------------");
            for (int i = tableOfRecords.Table.Length - 1; i > 0; i--)
            {
                Console.WriteLine("{0}: {1}", tableOfRecords.Table[i].Name, tableOfRecords.Table[i].Score);
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

        public static void Congratulations(int score)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.BackgroundColor = ConsoleColor.Black;
            string path = "0.txt";
            int fiter = 0;
            while (true)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write("New Record!!! Your score: {0}                            (press \"Esc\" to exit)",score);
                string[] text = File.ReadAllLines("txts\\" + path);
                for (int i = 0; i < text.Length; i++)
                    Console.WriteLine(text[i]);
                fiter++;
                path = Convert.ToString(fiter) + ".txt";
                if (fiter == 58)                
                    fiter = 0;
                Thread.Sleep(10);

                if (Console.KeyAvailable)
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)                    
                        break;
            }
        }        
    }
}
