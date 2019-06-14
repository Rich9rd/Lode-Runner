using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inertia3_WF
{
    class Player
    {

        //КООРДИНАТЫ ИГРОКА
        public byte X { get; set; }
        public byte Y { get; set; }

        public string name;
        public int score = 0;
        private Map map;
        public Game game = new Game();
        public Field field = new Field(100, 100);
        public int q = 0, e = 0;
        
        public Player(Map GameMap, Game Game)
        {
            game = Game;
            map = GameMap;
            this.field = GameMap.field;
            this.X = map.X;
            this.Y = map.Y;
        }
        
        public void Draw()
        {   
            int x = 0, y = 0;
            for (int i = 0; i < map.Widht; i++)
            {
                for (int j = 0; j < map.Hight; j++)
                {
                    if (X == i && Y == j)
                    {
                        field[i, j] = new player();
                    }

                    if (i == q && j == e)
                    {
                        field[i, j] = new Empty();
                    }
                }
                x = 0;
                y += 50;
            }
        }
        
        public void Draw(Graphics g)
        {
            int x = 0, y = 0;
            for (int i = 0; i < map.Widht; i++)
            {
                for (int j = 0; j < map.Hight; j++)
                {
                    if (X == i && Y == j)
                    {
                        field[i, j] = new player();
                    }

                    
                    g.DrawImage(field[i, j].bitmap, new Rectangle(x, y, 50, 50));
                    x += 50;
                    //field[i, j].Draw();
                }
                x = 0;
                y += 50;
            }
        }
        
        
    }
}
