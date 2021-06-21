using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DZ_4_Queue
{
    public class MyQueue<T> : IQueue<T>
    {
        public int Count => count;

        private Stack<T> direct;
        private Stack<T> reverse;
        private bool status;
        private int count;

        public MyQueue()
        {
            direct = new Stack<T>();
            count = 0;
        }
        
        public void Enqueue(T item)
        {
            if (status)
            {
                direct = new Stack<T>(reverse);
            }
            direct.Push(item);
            count++;
            status = false;
        }

        public T Dequeue()
        {
            if (!status)
            {
                reverse = new Stack<T>(direct);
            }
            T dq = reverse.Pop();
            count--;
            status = true;
            return dq;
        }

        public T Peek()
        {
            T ans = this.Dequeue();
            reverse.Push(ans);
            return ans;
        }
        
    }
}