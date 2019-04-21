using System;

namespace LRCN
{
    public class Pair<T, U>
    {
        public Pair()
        {
        }

        public Pair(T x, U y)
        {
            this.X = x;
            this.Y = y;
        }

        public T X { get; set; }
        public U Y { get; set; }
    };
}