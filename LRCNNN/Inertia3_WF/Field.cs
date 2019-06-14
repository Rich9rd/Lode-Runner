using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inertia3_WF
{
    class Field
    {
        Objects[,] field;
        public Field(int a, int b)
        {
            field = new Objects[a, b];
        }
        //Индексатор для получения элемента на поле
        public Objects this[int index1, int index2]
        {
            get
            {
                return field[index1, index2];
            }
            set
            {
                field[index1, index2] = value;
            }
        }
    }
}
