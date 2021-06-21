namespace DZ_4_Queue
{
    public interface IQueue<T>
    {
        int Count { get; }
        void Enqueue(T item);
        T Dequeue();
        T Peek();
    }
}