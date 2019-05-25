using System;

namespace LRCN
{
    public class DestroyedWall : Point
    {
        public bool IsActive { set; get; }        
        public int numOfTurn { set; get; }

        public DestroyedWall(bool isactive = false, int X = 0, int Y = 0, int numofturn = 0) : base(X, Y)
        {            
            this.numOfTurn = numofturn;
            this.IsActive = isactive;
        }
    }

    public struct ListOfDestroyedWalls
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
            if (listwall.Length > 0)
            {
                for (int i = listwall.Length - 1; i > 0; i--)
                {
                    listwall[i] = listwall[i - 1];
                }
            }
            listwall[0] = wall;
        }
    }
}