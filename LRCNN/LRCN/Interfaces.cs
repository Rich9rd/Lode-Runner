using System;

namespace LRCN
{
    interface ISelfMovable
    {
        void ChooseDirection(Map map);        
    }

    interface ICanInteract
    {
        void Hit(Map map);
    }
    
}