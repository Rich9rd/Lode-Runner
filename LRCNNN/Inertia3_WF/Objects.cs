using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inertia3_WF
{
    abstract class Objects          //Базовый абстрактный класс
    {
        public string icon;
        public Bitmap bitmap;
        public ConsoleColor frontColor { set; get; }
        public ConsoleColor backColor { set; get; }

        public void Draw()
        {
            Console.ForegroundColor = this.frontColor;
            Console.BackgroundColor = this.backColor;
            Console.Write(this.icon);
        }
    }

    class Wall : Objects
    {
        public Wall()
        {
            bitmap = Resource1.wall;
            icon = "# ";
            frontColor = ConsoleColor.DarkGray;
            backColor = ConsoleColor.Black;
            
        }

        public new void Draw()      //Перекрытый метод
        {
            Console.ForegroundColor = this.frontColor;
            Console.BackgroundColor = this.backColor;
            Console.Write(this.icon);
        }
    }

    class Trap : Objects
    {
        public Trap()
        {
            bitmap = Resource1.Empty;
            icon = "% ";
            frontColor = ConsoleColor.DarkRed;
            backColor = ConsoleColor.Black;
        }

        public new void Draw()
        {
            Console.ForegroundColor = this.frontColor;
            Console.BackgroundColor = this.backColor;
            Console.Write(this.icon);
        }
    }

    class Prize : Objects
    {
        public Prize()
        {
            bitmap = Resource1.Prize;
            icon = "@ ";
            frontColor = ConsoleColor.Yellow;
            backColor = ConsoleColor.Black;
        }

        public new void Draw()
        {
            Console.ForegroundColor = this.frontColor;
            Console.BackgroundColor = this.backColor;
            Console.Write(this.icon);
        }
    }

    class Stop : Objects
    {
        public Stop()
        {
            bitmap = Resource1.Empty;
            icon = ". ";
            frontColor = ConsoleColor.Magenta;
            backColor = ConsoleColor.Black;
        }

        public new void Draw()
        {
            Console.ForegroundColor = this.frontColor;
            Console.BackgroundColor = this.backColor;
            Console.Write(this.icon);
        }
    }

    class Empty : Objects
    {
        public Empty()
        {
            bitmap = Resource1.Empty;
            icon = "  ";
            frontColor = ConsoleColor.White;
            backColor = ConsoleColor.Black;
        }

        public new void Draw()
        {
            Console.ForegroundColor = this.frontColor;
            Console.BackgroundColor = this.backColor;
            Console.Write(this.icon);
        }
    }

    class stairs : Objects
    {
        public stairs()
        {
            bitmap = Resource1.Stairs;
            icon = "I ";
            frontColor = ConsoleColor.Cyan;
            backColor = ConsoleColor.Black;
        }

        public new void Draw()
        {
            Console.ForegroundColor = this.frontColor;
            Console.BackgroundColor = this.backColor;
            Console.Write(this.icon);
        }
    }

    class player : Objects
    {
        public player()
        {
            bitmap = Resource1.player;
            icon = "I ";
            frontColor = ConsoleColor.Cyan;
            backColor = ConsoleColor.Black;
        }

        public new void Draw()
        {
            Console.ForegroundColor = this.frontColor;
            Console.BackgroundColor = this.backColor;
            Console.Write(this.icon);
        }
    }
}
