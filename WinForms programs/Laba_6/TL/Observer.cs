using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsApplication1
{
    //          MVC

    // Паттерн Observer предлагает проект механизма публикации / подписки среди объектов. 
    // Он позволяет объекту (наблюдателю) зарегистрировать свою заинтересованность в
    // другом объекте (наблюдаемом).
    //  Каждый раз, когда наблюдаемый хочет уведомить своих наблюдателей об изменении,
    //  он применит к наблюдателю метод Update() 

    public interface IObserver
    {
        void UpdateState();
    }
}
