using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;

namespace LRC
{
    class Program
    {        
        static string[,] Map = {
            {"#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" },
            {"#", " ", " ", " ", " ", " ", " ", " ", " ", "|", " ", " ", " ", " ", " ", "|", " ", " ", " ", " ", " ", " ", "#" },
            {"#", "#", "|", "#", "#", "#", "|", "#", "#", "|", "#", "#", "#", "#", "#", "|", "#", "#", "#", "|", "#", "#", "#" },
            {"#", " ", "|", " ", " ", " ", "|", " ", " ", "|", " ", " ", " ", " ", "#", "|", " ", "#", " ", "|", " ", " ", "#" },
            {"#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "|", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" },
            {"#", " ", " ", " ", " ", " ", " ", " ", " ", " ", " ", "#", "|", " ", " ", " ", "|", "#", " ", " ", " ", " ", "#" },
            {"#", "#", "#", "|", "#", "#", "#", "#", "#", "#", "|", "#", "|", "#", "#", "#", "|", "#", "#", "|", "#", "|", "#" },
            {"#", " ", " ", "|", " ", " ", " ", " ", " ", " ", "|", " ", "|", " ", " ", " ", "|", " ", " ", "|", " ", "|", "#" },
            {"#", "#", "#", "#", "#", " ", "#", "#", "|", "#", "#", "#", "|", "#", "#", "#", "#", "#", "#", "|", "#", "#", "#" },
            {"#", " ", " ", " ", " ", " ", " ", " ", "|", "#", " ", " ", "|", " ", " ", " ", " ", " ", " ", "|", " ", " ", "#" },
            {"#", "#", "#", "#", "#", " ", "#", "#", "|", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "|", "#", "#", "#" },
            {"#", " ", " ", " ", " ", " ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", "#", " ", " ", "|", " ", " ", "#" },
            {"#", "#", "#", "#", "|", "#", "#", "#", "#", "#", "#", "#", "#", "#", "|", "#", "#", "#", "#", "#", "#", "#", "#" },
            {"#", " ", " ", " ", "|", " ", "#", " ", " ", " ", " ", " ", " ", " ", "|", " ", " ", " ", " ", " ", " ", " ", "#" },
            {"#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#", "#" }
        };

        static string[] ArrOfDirs = { "RIGHT", "RIGHT", "RIGHT", "RIGHT", "RIGHT", "RIGHT", "RIGHT", "RIGHT", "RIGHT", "RIGHT" };

        public class Pair<T, U>
        {
            public Pair()
            {
            }

            public Pair(T first, U second)
            {
                this.First = first;
                this.Second = second;
            }

            public T First { get; set; }
            public U Second { get; set; }
        };

        struct RecordsTable
        {
            public bool NewRecord { set; get; }
            public char Separator { set; get; }
            public string CurName { set; get; }
            public int CurScore { set; get; }
            public string Namestxt { set; get; }
            public string Resultstxt { set; get; }
            public string[] names_list;
            public string[] results_list;
            public RecordsTable(int curscore = 0, string curname = "Player", string namestxt = "Names.txt", string resultstxt = "Results.txt", char separator = '$', bool newrecord = false)
            {
                this.CurName = curname;
                this.CurScore = curscore;
                this.Namestxt = namestxt;
                this.Resultstxt = resultstxt;
                this.Separator = separator;
                this.NewRecord = newrecord;
                names_list = File.ReadAllText(Namestxt).Split(Separator);
                results_list = File.ReadAllText(Resultstxt).Split(Separator);
            }

            int GetLength()
            {
                int counter = 0;
                foreach (string elem in names_list)
                {
                    if (elem == null)
                    {
                        break;
                    }
                    counter++;
                }
                if (counter > 10)
                {
                    return 10;
                }
                return counter;
            }

            Pair<string, int>[] ConvertStringArrToIntAndMakeListOfPairs(string[] strArr)
            {
                Pair<string, int>[] listOfPairs = new Pair<string, int>[10];
                for (int i = 0; i < GetLength(); i++)
                {
                    listOfPairs[i] = new Pair<string, int>(names_list[i], Convert.ToInt32(strArr[i]));                    
                }
                return listOfPairs;
            }

            public void AddResult()
            {
                Pair<string, int>[] listOfPairs = ConvertStringArrToIntAndMakeListOfPairs(results_list);
                bool alreadyInList = false;
                int pos = 0;
                foreach (string name in names_list)
                {
                    if (name == CurName)
                    {
                        alreadyInList = true;
                        break;
                    }
                    pos++;
                }

                if (alreadyInList)
                {
                    if (CurScore > listOfPairs[pos].Second)
                    {
                        NewRecord = true;
                        listOfPairs[pos].Second = CurScore;
                    }
                }

                
            }
        }
        


        class Player
        {
            public bool falling { set; get; }
            public int X { set; get; }
            public int Y { set; get; }
            public int pX { set; get; }
            public int pY { set; get; }
            public int nX { set; get; }
            public int nY { set; get; }
            public int Score { set; get; }
            public int Counter { set; get; }
            public string Dir { set; get; }
            public string preSym { set; get; }

            public ListOfDestroyedWalls Dwalls = new ListOfDestroyedWalls();            
            public Player(int x = 1, int y = 1, int px = 1, int py = 1, int nx = 1, int ny = 1, int score = 0, int counter = 0, string dir = "RIGHT", string presym = " ")
            {
                this.X = x;
                this.Y = y;
                this.pX = px;
                this.pY = py;
                this.nX = nx;
                this.nY = ny;
                this.Dir = dir;
                this.Score = score;
                this.Counter = counter;
                this.preSym = presym;
            }

            public void DrawMe()
            {
                Map[this.X, this.Y] = "I";
            }

            public void DrawPreSym()
            {
                if (this.preSym == "|")
                {
                    Map[this.pX, this.pY] = "|";
                }
                else
                {
                    Map[this.pX, this.pY] = " ";
                }
            }

            public bool isMove()
            {
                switch (Map[this.nX, this.nY])
                {
                    case " ":
                        if (this.Dir == "RIGHT" || this.Dir == "LEFT")
                        {
                            return true;
                        }
                        else if (this.Dir == "UP" || this.Dir == "DOWN")//Down не нужно
                        {
                            if (this.preSym == "|")
                            {
                                return true;
                            }
                            return false;
                        }
                        break;
                    case "#":
                        return false;
                        break;
                    case "|":
                        return true;
                        break;
                    case "@":
                        this.Score++;
                        //Console.Beep();
                        return true;
                        break;
                    default:
                        return true;
                        break;
                }
                return true;
            }

            public void Move(ConsoleKeyInfo keyInfo)
            {
                this.falling = false;
                if (Map[this.X + 1, this.Y] == "#" || Map[this.X + 1, this.Y] == "|" || this.preSym == "|")
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.S:
                            this.Dir = "DOWN";
                            this.nX = this.X + 1;
                            if ((this.preSym != "|" || Map[this.nX, this.nY] == "#") && Map[this.nX, this.nY] != "|")//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                            {
                                if (ArrOfDirs[0] == "RIGHT" && Map[this.nX, this.nY + 1] == "#")
                                {
                                    Map[this.nX, this.nY + 1] = " ";
                                    DestroyedWall tempWall = new DestroyedWall(true, this.nX, this.nY + 1, this.Counter);
                                    Dwalls.AddWall(tempWall);
                                }
                                else if (ArrOfDirs[0] == "LEFT" && Map[this.nX, this.nY - 1] == "#")
                                {
                                    Map[this.nX, this.nY - 1] = " ";
                                    DestroyedWall tempWall = new DestroyedWall(true, this.nX, this.nY - 1, this.Counter);
                                    Dwalls.AddWall(tempWall);
                                }
                            }
                            break;
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.W:
                            this.Dir = "UP";
                            this.nX = this.X - 1;
                            break;
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.A:
                            this.Dir = "LEFT";
                            this.nY = this.Y - 1;
                            break;
                        case ConsoleKey.RightArrow:
                        case ConsoleKey.D:
                            this.Dir = "RIGHT";
                            this.nY = this.Y + 1;
                            break;
                        default:
                            break;
                    }

                    this.pX = this.X;
                    this.pY = this.Y;

                    if (this.isMove())
                    {
                        this.X = this.nX;
                        this.Y = this.nY;

                    }
                    else
                    {
                        this.nX = this.X;
                        this.nY = this.Y;
                    }
                }
                else
                {
                    this.falling = true;
                    this.pX = this.X;
                    this.pY = this.Y;
                    this.X++;
                    this.nX = this.X;
                    if (Map[this.X, this.Y] == "@")
                    {
                        this.Score++;
                        //Console.Beep();
                    }
                }//Falling             

                this.DrawPreSym();
                this.preSym = Map[this.X, this.Y];
                this.DrawMe();
            }
        }
        
        class DestroyedWall
        {
            public bool IsActive { set; get; }
            public int X { set; get; }
            public int Y { set; get; }
            public int numOfTurn { set; get; }

            public DestroyedWall(bool isactive = false, int x = 0, int y = 0, int numofturn = 0)
            {
                this.X = x;
                this.Y = y;
                this.numOfTurn = numofturn;
                this.IsActive = isactive;
            }
        }

        struct ListOfDestroyedWalls
        {
            DestroyedWall[] listwall;
            public int getLength()
            {
                return listwall.Length;
            }
            public ListOfDestroyedWalls(DestroyedWall[] walls)
            {
                listwall = walls;
            }
            public DestroyedWall this[int i]
            {
                set { listwall[i] = value; }
                get { return listwall[i]; }
            }
            public void AddWall(DestroyedWall wall)
            {
                if(listwall.Length > 0)
                {
                    for (int i = listwall.Length - 1; i > 0; i--)
                    {
                        listwall[i] = listwall[i - 1];
                    }
                }                
                listwall[0] = wall;
            }
        }        

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            //sp.Play();
            bool gameStatus = true;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            Console.WriteLine("Hello! enter your name:");
            string PlayerName = Console.ReadLine();

            randomPlayerSpawn();

            int lodenum = 10;
            randomLodeSpawn(lodenum);

            int[] playerStartCoords = playerSearch();

            Player Hero = new Player(playerStartCoords[0], playerStartCoords[1], playerStartCoords[0], playerStartCoords[1], playerStartCoords[0], playerStartCoords[1]);

            DestroyedWall[] tempDlist = { new DestroyedWall(false), new DestroyedWall(false), new DestroyedWall(false), new DestroyedWall(false), new DestroyedWall(false)};
            Hero.Dwalls = new ListOfDestroyedWalls(tempDlist);

            render();



            while (Hero.preSym != "O")
            {
                try
                {
                    do
                    {
                        if (!Hero.falling)
                        {
                            keyInfo = Console.ReadKey();
                        }                        

                        Hero.Move(keyInfo);
                        Hero.Counter++;
                        addToArr(transformKeyToStr(keyInfo), ArrOfDirs);

                        for (int i = 0; i < Hero.Dwalls.getLength(); i++)
                        {
                            if (Hero.Dwalls[i].IsActive && Hero.Counter - Hero.Dwalls[i].numOfTurn > 7)
                            {
                                if (Map[Hero.Dwalls[i].X, Hero.Dwalls[i].Y] == "I")//Lift Player Up
                                {
                                    Hero.X = Hero.Dwalls[i].X - 1;
                                    Hero.Y = Hero.Dwalls[i].Y;
                                    Hero.pX = Hero.Dwalls[i].X - 1;
                                    Hero.pY = Hero.Dwalls[i].Y;
                                    Hero.nX = Hero.Dwalls[i].X - 1;
                                    Hero.nY = Hero.Dwalls[i].Y;

                                    Map[Hero.X, Hero.Y] = "I";
                                }
                                Map[Hero.Dwalls[i].X, Hero.Dwalls[i].Y] = "#";
                            }
                        }
                        render();
                        Console.SetCursorPosition(0, Map.GetLength(0));
                        Console.WriteLine("Your score: {0} / {1}", Hero.Score, lodenum);
                        if (Hero.Score >= lodenum)
                        {
                            randomExitSpawn();
                        }
                    } while (Hero.falling);                    
                }
                catch
                {
                    gameStatus = false;
                }

            }

            Console.WriteLine("{0}, you won!!!", PlayerName);
            Console.WriteLine("you score: {0}", Hero.Score);
            Console.ReadKey();
        }

        static void render()
        {
            Console.SetCursorPosition(0, 0);

            for (int row = 0; row < Map.GetLength(0); row++)
            {
                for (int col = 0; col < Map.Length / Map.GetLength(0); col++)
                {
                    SetColor(Map[row, col]);
                    Console.Write(Map[row, col]);
                }
                Console.WriteLine();
            }

        }

        static void addToArr(string str, string[] arr)
        {
            for (int i = arr.Length - 1; i > 0; i--)
            {
                arr[i] = arr[i - 1];
            }
            arr[0] = str;
        }

        static string transformKeyToStr(ConsoleKeyInfo keyInfo)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                    return "DOWN";
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                    return "UP";
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                    return "LEFT";
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                    return "RIGHT";
                    break;
                default:
                    return "RIGHT";
                    break;
            }
        }

        static int[] playerSearch()
        {
            for (int row = 0; row < Map.GetLength(0); row++)
            {
                for (int col = 0; col < Map.Length / Map.GetLength(0); col++)
                {
                    if (Map[row, col] == "I")
                    {
                        int[] ans = { row, col };
                        return ans;
                    }
                }
            }
            int[] ans2 = { 0, 0 };
            return ans2;
        }

        static void randomPlayerSpawn()
        {
            Random rnd = new Random();
            int xp = 0;
            int yp = 0;
            while (Map[xp, yp] != " ")
            {
                xp = rnd.Next(Map.GetLength(0));
                yp = rnd.Next(Map.Length / Map.GetLength(0));
            }
            Map[xp, yp] = "I";
        }

        static void randomExitSpawn()
        {
            //Console.Beep();
            Random rnd = new Random();
            int ran = rnd.Next(2);            
            Map[Map.Length / Map.GetLength(0) - 1, ran * (Map.GetLength(0) - 1)] = "O";           
        }

        static void randomLodeSpawn(int num)
        {
            Random rnd = new Random();
            int xp = 0;
            int yp = 0;
            while (num-- != 0)
            {
                while (Map[xp, yp] != " ")
                {
                    xp = rnd.Next(Map.GetLength(0));
                    yp = rnd.Next(Map.Length / Map.GetLength(0));
                }
                Map[xp, yp] = "@";
            }
        }

        static void SetColor(string sym)
        {
            switch (sym)
            {
                case "I":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "|":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "@":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "#":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case " ":
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
            }
        }
    }
}
