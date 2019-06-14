using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inertia3_WF
{
    class Map
    {
        public byte Hight { get { return high; } } // ВЫСОТА
        public byte Widht { get; private set; }//ШИРИНА
        public byte X { get; set; }
        public byte Y { get; set; }

        Game game = new Game();
        Random rnd = new Random();
        private byte high;

        public Map(byte hight, byte widht)
        {
            high = hight;
            Widht = widht;
            X = (byte)rnd.Next(0, Widht);
            Y = (byte)rnd.Next(0, Hight);
        }

        public Field field = new Field(100, 100);

        public int prizes = 0;
        public int collectedprizes = 0;
        public int turns = 0;
        public int cells = 0;

        //Генерируем поле
        public void Generate()
        {
            game.win = false;
            prizes = 0;
            turns = 0;
            cells = 0;
            collectedprizes = 0;
            Random rand = new Random();
 
            //Console.Clear();

            for (int i = 0; i < Hight; i++)
            {
                for (int j = 0; j < Widht; j++)
                {
                    field[i, j] = new Empty();
                }
            }

            for (int i = 0; i < Hight; i++)
            {
                for (int j = 0; j < Widht; j+=2)
                {
                    field[j, i] = new Wall();
                }
            }

            for (int i = 1; i < Hight -1; i++)
            {
                for (int j = 1; j < Widht; j += 2)
                {
                    Random rnd = new Random();
                    int tempInt = rnd.Next(100);
                    if (tempInt > 50)
                    {
                        field[j, i] = new Prize();
                    }                    
                }
            }

            for (int row = 0; row < Hight; row++)
            {
                field[row, 0] = new Wall();
            }
            for (int row = 0; row < Hight; row++)
            {
                field[row, Widht - 1] = new Wall();
            } // генерация правой и левой стенки            

            //Random rnd = new Random();

            field[2, 3] = new stairs();
            field[3, 3] = new stairs();

            field[4, 2] = new stairs();
            field[5, 2] = new stairs();


            field[7, 8] = new stairs();
            field[8, 8] = new stairs();

        }

    }

    public class Point
    {
        public int X { set; get; }
        public int Y { set; get; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
