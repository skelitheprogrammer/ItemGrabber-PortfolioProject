public interface IObserver<T>
{
    void Subscribe(T sub);
    void Unsubscribe(T sub);
}