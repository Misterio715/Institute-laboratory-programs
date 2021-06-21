using System.Collections.Generic;

namespace DZ_4_Queue
{
    public class NewQueue<T> : IQueue<T>
    {
        private Queue<T> queue;

        public int Count => queue.Count;

        public NewQueue()
        {
            queue = new Queue<T>();
        }
        
        public NewQueue(Queue<T> queue)
        {
            this.queue = queue;
        }

        public void Enqueue(T item)
        {
            queue.Enqueue(item);
        }

        public T Dequeue()
        {
            return queue.Dequeue();
        }

        public T Peek()
        {
            return queue.Peek();
        }
    }
}