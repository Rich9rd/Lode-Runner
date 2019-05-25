using System;

namespace LRCN
{
    public class Cell
    {        
        public gameElements description { set; get; }
        public ConsoleColor frontColor { set; get; }
        public ConsoleColor backColor { set; get; }
        public char symbol { set; get; }
        public Cell(gameElements Description, char Symbol = ' ', ConsoleColor FrontColor = ConsoleColor.White, ConsoleColor BackColor = ConsoleColor.Black)
        {            
            description = Description;
            DefoultCellSettings();
        }
        public void DefoultCellSettings()
        {
            Random rnd = new Random();
            switch (description)
            {
                case gameElements.Empty:                    
                    frontColor = ConsoleColor.White;
                    backColor = ConsoleColor.Black;
                    symbol = ' ';
                    break;
                case gameElements.Player:
                    frontColor = ConsoleColor.Cyan;
                    backColor = ConsoleColor.Black;
                    symbol = 'I';
                    break;                
                case gameElements.Enemy:
                    frontColor = ConsoleColor.Red;
                    backColor = ConsoleColor.Black;
                    symbol = rnd.Next(2) == 0 ? '[' : ']';
                    break;
                case gameElements.Wall:
                    frontColor = ConsoleColor.DarkGray;
                    backColor = ConsoleColor.DarkGray;
                    symbol = '#';
                    break;
                case gameElements.Coin:
                    frontColor = ConsoleColor.Yellow;
                    backColor = ConsoleColor.Black;
                    symbol = '@';
                    break;
                case gameElements.SuperCoin:
                    frontColor = ConsoleColor.Yellow;
                    backColor = ConsoleColor.Black;
                    symbol = '9';
                    break;
                case gameElements.Trap:
                    frontColor = ConsoleColor.DarkRed;
                    backColor = ConsoleColor.Black;
                    symbol = '~';
                    break;
                case gameElements.Bomb:
                    frontColor = ConsoleColor.DarkRed;
                    symbol = '&';
                    break;
                case gameElements.Stair:
                    frontColor = ConsoleColor.Gray;
                    backColor = ConsoleColor.Black;
                    symbol = 'H';
                    break;
                case gameElements.Teleport:
                    frontColor = ConsoleColor.Blue;
                    backColor = ConsoleColor.Black;
                    symbol = rnd.Next(2) == 0 ? '(' : ')';
                    break;
                case gameElements.Exit :
                    frontColor = ConsoleColor.Magenta;
                    backColor = ConsoleColor.Black;
                    symbol = 'O';
                    break;
                case gameElements.Corpse:
                    frontColor = ConsoleColor.Magenta;
                    backColor = ConsoleColor.Black;
                    symbol = '_';
                    break;
                case gameElements.Shovel:
                    frontColor = ConsoleColor.Yellow;
                    backColor = ConsoleColor.Blue;
                    symbol = 'S';                    
                    break;
                case gameElements.Pickaxe:
                    frontColor = ConsoleColor.Yellow;
                    backColor = ConsoleColor.Blue;
                    symbol = 'P';
                    break;
                case gameElements.Bridge:
                    frontColor = ConsoleColor.Blue;
                    backColor = ConsoleColor.Blue;
                    symbol = '=';
                    break;
                default:
                    break;
            }
        }
        public void CopyCell(Cell Copied)
        {            
            symbol = Copied.symbol;
            backColor = Copied.backColor;
            frontColor = Copied.frontColor;
            description = Copied.description;            
        }
        public void DrawCell()
        {
            Console.ForegroundColor = frontColor;
            Console.BackgroundColor = backColor;
            Console.Write("{0} ", symbol);        
        }
    }
}