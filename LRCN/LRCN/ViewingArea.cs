using System;

namespace LRCN
{
    public class ViewingArea
    {
        public int Up { set; get; }
        public int Down { set; get; }
        public int Left { set; get; }
        public int Right { set; get; }               
        public int Area { get; } // half        
        public ViewingArea(Map map, Player player, int area = 12)
        {       
            Area = area;
            MoveViewingArea(map, player);
        }

        public bool IsInto(GameObject gameObject)
        {
            return Up <= gameObject.X && Down >= gameObject.X && Left <= gameObject.Y && Right >= gameObject.Y;
        }

        public void MoveViewingArea(Map map, Player player)
        {
            Up = player.X - Area;
            Down = player.X + Area;
            Left = player.Y - Area;
            Right = player.Y + Area;

            if (Up < 0)
            {
                Up = 0;
                Down = Area * 2;
            }
            if (Down >= map.Height)
            {
                Down = map.Height - 1;
                Up = map.Height - 1 - Area * 2;
            }
                
            if (Left < 0)
            {
                Left = 0;
                Right = Area * 2;
            }
                
            if (Right >= map.Width)
            {
                Right = map.Width - 1;
                Left = map.Width - 1 - Area * 2;
            }
                
        }
    }
}